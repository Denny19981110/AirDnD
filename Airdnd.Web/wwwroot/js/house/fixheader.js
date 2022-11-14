window.addEventListener("scroll", function () {
    let HostInfoHeight = document.querySelector(".host-info-wrap").getBoundingClientRect().y;
    let HostInfo = document.querySelector(".host-info-wrap").getBoundingClientRect().bottom;
    let secondheader = document.querySelector(".second-header").getBoundingClientRect().bottom;
    if (document.body.clientWidth > 768) {
    if (HostInfoHeight <= 0) {
        document.querySelector(".second-header").style.display = "block";
    } else {
        document.querySelector(".second-header").style.display = "none";
    }
    if (HostInfo <= secondheader) {
        document.querySelector(".second-header .txt-wrap").style.display = "flex";
    } else {
        document.querySelector(".second-header .txt-wrap").style.display = "none";
    }}
});