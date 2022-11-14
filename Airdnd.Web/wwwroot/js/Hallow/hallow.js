// computed: {
//   total: function() {
//     var sum = 0;
//     this.questions.forEach(function (v) {
//       sum += Number(v.selected);
//     });
//     return sum;
//   }
// },
// methods: {
//   settlement: function() {
//     alert('總分: ' + this.total);
//   },
//   reset: function() {
//     this.questions.forEach(function (v) {
//       v.selected = null;
//     });
//   }
// },
// });
var vm = new Vue({
  el: '#app',
  data: {
    questionList:[],
    answerList:[],
    pageIndex:1,
    sum:0,
    answer:0,
    numberOfQuestions:0,
  },
  methods: {
    screenshot(){
      capture()
    },
    goNext(value,i){
      this.questionList[this.numberOfQuestions].selected=i
      if(this.numberOfQuestions<2){
        this.numberOfQuestions++
      }
    },
    goNextBtn(){ 
      this.numberOfQuestions++;
    },
    showAnswer(){
      if(this.questionList.every(x=>x.selected!=null) && this.numberOfQuestions==2){
        return true
      }
      return false
    },
    goAnswer(){
      let ansL=this.questionList.map(x=>x.option[x.selected].value)
      this.sum=ansL.reduce((a,b)=>a+b)
      this.answer=this.sum%4
      this.pageIndex++
    },
    activeClass(i){
      if(i==this.questionList[this.numberOfQuestions].selected){
        return "activedOption"
      }
      return null
    }
  },
  async created() {
      try {
          let question = await axios.get("../../../assert/Double11/test11.json")
          let answer = await axios.get("../../../assert/Double11/answer11.json")
          this.questionList = question.data
          this.answerList = answer.data
      } catch (e) {
          console.log(e)
      }
  }
})
function capture(){
  
  const captureElement = document.querySelector('#capture')
  const houseBtn = document.querySelector('#houseBtn')
  captureElement.style.backgroundColor="#000"
  captureElement.style.borderRadius = '0em';
  houseBtn.style.display="none"
  html2canvas(captureElement)
        .then(canvas => {
            canvas.style.display = 'none'
            document.body.appendChild(canvas)
            return canvas
        })
        .then(canvas => {
            const image = canvas.toDataURL('image/png').replace('image/png', 'image/octet-stream')
            const a = document.createElement('a')
            a.setAttribute('download', 'Halloween.png')
            a.setAttribute('href', image)
            a.click()
            canvas.remove()
        })
        captureElement.style.backgroundColor="transparent"
        houseBtn.style.display="block"
        captureElement.style.borderRadius = '1em';
}