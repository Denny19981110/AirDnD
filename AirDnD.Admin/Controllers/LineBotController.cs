using Airdnd.Core.Entities;
using Airdnd.Core.Interfaces;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System;
using isRock.LineBot;
using System.Linq;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using System.Net.Http;


namespace Airdnd.Admin.Controllers
{
    public class LineBotController : LineWebHookControllerBase
    {
        private readonly string Token = "cD8EijnNYmf9lVHNNeWIF8fhyIrKgs7grQDpc808EQI7mi7TL4W+ICc+Vi/1cDCd+lj06aBgOZ+p5BFUrZQ9HPKe4Uf0oX6hw7b+dF+LewQSho/up54MCE/XUxYCZB6JnmX4MXFPBs5yHOIpq/GmoAdB04t89/1O/w1cDnyilFU=";
        private readonly string Id = "U897a6ad771ea267a2724d31078a6c3e3";
        private readonly IRepository<Listing> _listing;
        private readonly IRepository<ListingImage> _listingImage;
        const string endpoint = "https://cttest2022denny.cognitiveservices.azure.com/";
        const string key = "25349cdfef9c4db2adeb47b7c13c8c42";

        public LineBotController(IRepository<Listing> listing, IRepository<ListingImage> listingImage)
        {

            _listing = listing;
            _listingImage = listingImage;
        }


        public IActionResult WebHook()
        {
            return View();
        }

        [HttpPost]
        
        public IActionResult LineWebHook()
        {
            var LineEvent = this.ReceivedMessage.events.FirstOrDefault();

            //新朋友來了(或解除封鎖)
            if (LineEvent.type.ToLower() == "follow")
            {
                
                Bot followbot = new Bot(Token);
                // userId = 
                var userInfo = followbot.GetUserInfo(ReceivedMessage.events.FirstOrDefault().source.userId);
                followbot.ReplyMessage(ReceivedMessage.events.FirstOrDefault().replyToken, $"哈，'{userInfo.displayName}' 你來了...歡迎");
                //LineUserId.Add(userInfo.userId);
                return Ok();
            }
            return Ok();
        }

        //[HttpPost]
        //public IActionResult LineWebHook()
        //{
        //    List<string> LineUserId = new List<string>();
        //    LineUserId.Add(Id);
        //    var LineEvent = this.ReceivedMessage.events.FirstOrDefault();

        //    if(LineEvent.type.ToLower() == "follow")
        //    {
        //        //新朋友來了(或解除封鎖)
        //        Bot followbot = new Bot(Token);
        //        //var userId = 
        //        var userInfo = followbot.GetUserInfo(ReceivedMessage.events.FirstOrDefault().source.userId);
        //        followbot.ReplyMessage(ReceivedMessage.events.FirstOrDefault().replyToken, $"哈，'{userInfo.displayName}' 你來了...歡迎");
        //        LineUserId.Add(userInfo.userId);
        //        return Ok();
        //    }
        //    var responseMsg = "";
        //    //if (LineEvent.type.ToLower() == "message" && LineEvent.message.type == "text")
        //    //{
        //    //    responseMsg = $"你說了: {LineEvent.message.text}";
        //    //    this.ReplyMessage(LineEvent.replyToken, responseMsg);
        //    //    return Ok();
        //    //}

        //    //取得Line Event
           
        //    return Ok();
        //    //if (this.ReceivedMessage.events.FirstOrDefault().type == "follow")
        //    //{
        //    //    //新朋友來了(或解除封鎖)
        //    //    isRock.LineBot.Bot followbot = new isRock.LineBot.Bot(Token);
        //    //    //var userId = 
        //    //    var userInfo = followbot.GetUserInfo(ReceivedMessage.events.FirstOrDefault().source.userId);
        //    //    followbot.ReplyMessage(ReceivedMessage.events.FirstOrDefault().replyToken, $"哈，'{userInfo.displayName}' 你來了...歡迎");
        //    //    LineUserId.Add(userInfo.userId);
        //    //    return Ok();
        //    //}

        //    //return View();
        //}

        //[HttpPost]
        //public IActionResult LineWebHook(CarouselDto Carouselinputs, LineBesicDto input)
        //{

        //    List<string> LineUserId = new List<string>();
        //    LineUserId.Add(Id);
        //    if (ReceivedMessage.events.FirstOrDefault().type == "follow")
        //    {
        //        //新朋友來了(或解除封鎖)
        //        isRock.LineBot.Bot followbot = new isRock.LineBot.Bot(ChannelAccessToken);
        //        //var userId = 
        //        var userInfo = followbot.GetUserInfo(ReceivedMessage.events.FirstOrDefault().source.userId);
        //        followbot.ReplyMessage(ReceivedMessage.events.FirstOrDefault().replyToken, $"哈，'{userInfo.displayName}' 你來了...歡迎");
        //        LineUserId.Add(userInfo.userId);
        //        return Ok();
        //    }

        //    if (input.Message != null)
        //    {
        //        isRock.LineBot.Bot LineBot = new isRock.LineBot.Bot(Token);

        //        LineBot.PushMessage(Id, input.Message);
        //    }

        //    if ((Carouselinputs.text != null) && (Carouselinputs.roomName != null) && (Carouselinputs.title != null))
        //    {
        //        //Bot基本宣告
        //        isRock.LineBot.Bot CarouselBot = new isRock.LineBot.Bot(Token);
        //        var actions = new List<isRock.LineBot.TemplateActionBase>();
        //        var CarouselTemplate = new isRock.LineBot.CarouselTemplate();

        //        //房源圖片
        //        var roomNames = Carouselinputs.roomName.Select(x => x).ToList();
        //        var roomImage = new List<string>();
        //        var roomId = new List<int>();

        //        foreach (var name in roomNames)
        //        {
        //            if (name != null)
        //            {
        //                var room = _listing.GetAll().First(x => x.ListingName == name);
        //                roomId.Add(room.ListingId);
        //                var Url = _listingImage.GetAll().First(x => x.ListingId == room.ListingId).ListingImagePath.ToString();
        //                roomImage.Add(Url);
        //            }
        //        }

        //        //組Template
        //        for (var i = 0; i < roomId.Count; i++)
        //        {
        //            actions.Add(new isRock.LineBot.UriAction() { label = "點擊進入", uri = new Uri($"https://airdnd-frontend.azurewebsites.net/House/{Carouselinputs.roomName[i]}") });
        //            var Column = new isRock.LineBot.Column
        //            {

        //                text = Carouselinputs.text[i],
        //                title = Carouselinputs.title[i],
        //                //設定圖片
        //                thumbnailImageUrl = new Uri(roomImage[i]),
        //                actions = actions //設定回覆動作
        //            };
        //            CarouselTemplate.columns.Add(Column);
        //        }
        //        ReplyMessage(Token, CarouselTemplate);



        //        //CarouselBot.PushMessage(Id, CarouselTemplate);





        //    }



        //    return RedirectToAction("WebHook");
        //}


        public class LineBesicDto
        {
            public string Message { get; set; }
            public List<string> Photo { get; set; }
        }
        public class CarouselDto
        {
            //CarouselTemplate
            public List<string> roomName { get; set; }
            public List<string> title { get; set; }
            public List<string> text { get; set; }
            public List<string> Url { get; set; }
        }

    }

    

}
