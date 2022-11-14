using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System.Collections.Generic;
using System.IO;
using System.Net;
using System.Text;
using System;
using isRock.LineBot;

namespace Airdnd.Admin.Controllers
{
    public class LineWebHookControllerBase : Controller
    {
        private string _ChannelAccessToken = string.Empty;

        private ReceivedMessage receivedMessage;

        private string channelAccessToken => ChannelAccessToken;

        public string ChannelAccessToken
        {
            get
            {
                return _ChannelAccessToken;
            }
            set
            {
                _ChannelAccessToken = value;
            }
        }

        public ReceivedMessage ReceivedMessage
        {
            get
            {
                if (receivedMessage == null)
                {
                    string rawData = "";
                    using (StreamReader streamReader = new StreamReader(Request.Body, Encoding.UTF8))
                    {
                        rawData = streamReader.ReadToEndAsync().Result;
                    }

                    receivedMessage = Utility.Parsing(rawData);
                }

                return receivedMessage;
            }
        }

        public string PushMessage(string ToUserID, MessageBase Message)
        {
            return GetBotInstance().PushMessage(ToUserID, Message);
        }

        public string PushMessage(string ToUserID, List<MessageBase> Messages)
        {
            return GetBotInstance().PushMessage(ToUserID, Messages);
        }

        public string PushMessagesWithJSON(string ToUserID, string JSONMessages)
        {
            return Utility.PushMessagesWithJSON(ToUserID, JSONMessages, channelAccessToken);
        }

        public string PushMessage(string ToUserID, ImagemapMessage Message)
        {
            return Utility.PushImageMapMessage(ToUserID, Message, channelAccessToken);
        }

        public string PushMessage(string ToUserID, ButtonsTemplate Message)
        {
            return Utility.PushTemplateMessage(ToUserID, Message, channelAccessToken);
        }

        public string PushMessage(string ToUserID, ConfirmTemplate Message)
        {
            return Utility.PushTemplateMessage(ToUserID, Message, channelAccessToken);
        }

        public string PushMessage(string ToUserID, CarouselTemplate Message)
        {
            return Utility.PushTemplateMessage(ToUserID, Message, channelAccessToken);
        }

        public string PushMessage(string ToUserID, string TextMessage)
        {
            if (TextMessage.Length < 0)
            {
                throw new Exception("訊息內容不正確");
            }

            if (TextMessage.Length > 1800)
            {
                throw new Exception("訊息內容太長");
            }

            return Utility.PushMessage(ToUserID, TextMessage, channelAccessToken);
        }

        public string PushMessage(string ToUserID, Uri ContentUrl)
        {
            return Utility.PushImageMessage(ToUserID, ContentUrl.ToString(), ContentUrl.ToString(), channelAccessToken);
        }

        public string PushMessage(string ToUserID, Uri originalContentUrl, Uri previewImageUrl)
        {
            return Utility.PushImageMessage(ToUserID, originalContentUrl.ToString(), previewImageUrl.ToString(), channelAccessToken);
        }

        public string PushMessage(string ToUserID, int packageId, int stickerId)
        {
            return Utility.PushStickerMessage(ToUserID, packageId, stickerId, channelAccessToken);
        }

        public string PushMulticast(List<string> ToUsers, List<string> TextMessages)
        {
            string text = "\r\n{{\r\n    'to': {0},\r\n    'messages': {1}\r\n}}\r\n";
            if (ToUsers.Count <= 0 || ToUsers.Count >= 150)
            {
                throw new Exception("ToUsers必須介於1-150之間");
            }

            if (TextMessages.Count <= 0 || TextMessages.Count >= 6)
            {
                throw new Exception("Messages必須介於1-5");
            }

            try
            {
                List<MulticastMessage> list = new List<MulticastMessage>();
                for (int i = 0; i < TextMessages.Count; i++)
                {
                    TextMessages[i] = TextMessages[i].Trim();
                    TextMessages[i] = TextMessages[i].Replace("\n", "\\n");
                    TextMessages[i] = TextMessages[i].Replace("\r", "\\r");
                    TextMessages[i] = TextMessages[i].Replace("\"", "'");
                    list.Add(new MulticastMessage
                    {
                        text = TextMessages[i],
                        type = "text"
                    });
                }

                for (int j = 0; j < ToUsers.Count; j++)
                {
                    ToUsers[j] = ToUsers[j].Trim();
                }

                string arg = JsonConvert.SerializeObject(ToUsers);
                string arg2 = JsonConvert.SerializeObject(list);
                text = text.Replace("'", "\"");
                text = string.Format(text, arg, arg2);
                WebClient webClient = new WebClient();
                webClient.Headers.Clear();
                webClient.Headers.Add("Content-Type", "application/json");
                webClient.Headers.Add("Authorization", "Bearer " + channelAccessToken);
                byte[] bytes = Encoding.UTF8.GetBytes(text);
                byte[] bytes2 = webClient.UploadData("https://api.line.me/v2/bot/message/multicast", bytes);
                return Encoding.UTF8.GetString(bytes2);
            }
            catch (WebException ex)
            {
                using StreamReader streamReader = new StreamReader(ex.Response!.GetResponseStream());
                string text2 = streamReader.ReadToEnd();
                throw new Exception("PushMulticast API ERROR: " + text2, ex);
            }
        }

        public string ReplyMessage(string ReplyToken, MessageBase Message)
        {
            return GetBotInstance().ReplyMessage(ReplyToken, Message);
        }

        public string ReplyMessage(string ReplyToken, List<MessageBase> Messages)
        {
            return GetBotInstance().ReplyMessage(ReplyToken, Messages);
        }

        public string ReplyMessageWithJSON(string ReplyToken, string JSONMessages)
        {
            return Utility.ReplyMessageWithJSON(ReplyToken, JSONMessages, channelAccessToken);
        }

        public string ReplyMessage(string ReplyToken, ImagemapMessage ImagemapMessage)
        {
            return Utility.ReplyImageMapMessage(ReplyToken, ImagemapMessage, channelAccessToken);
        }

        public string ReplyMessage(string ReplyToken, ConfirmTemplate ConfirmTemplate)
        {
            return Utility.ReplyTemplateMessage(ReplyToken, ConfirmTemplate, channelAccessToken);
        }

        public string ReplyMessage(string ReplyToken, CarouselTemplate CarouselTemplate)
        {
            return Utility.ReplyTemplateMessage(ReplyToken, CarouselTemplate, channelAccessToken);
        }

        public string ReplyMessage(string ReplyToken, ImageCarouselTemplate ImageCarouselTemplate)
        {
            return Utility.ReplyTemplateMessage(ReplyToken, ImageCarouselTemplate, channelAccessToken);
        }

        public string ReplyMessage(string ReplyToken, ButtonsTemplate ButtonsTemplate)
        {
            return Utility.ReplyTemplateMessage(ReplyToken, ButtonsTemplate, channelAccessToken);
        }

        public string ReplyMessage(string ReplyToken, string Message)
        {
            if (Message.Length < 0)
            {
                throw new Exception("訊息內容不正確");
            }

            if (Message.Length > 1800)
            {
                throw new Exception("訊息內容太長");
            }

            return Utility.ReplyMessage(ReplyToken, Message, channelAccessToken);
        }

        public string ReplyMessage(string ReplyToken, int packageId, int stickerId)
        {
            return Utility.ReplyStickerMessage(ReplyToken, packageId, stickerId, channelAccessToken);
        }

        public string ReplyMessage(string ReplyToken, Uri ContentUrl)
        {
            return Utility.ReplyImageMessage(ReplyToken, ContentUrl.ToString(), ContentUrl.ToString(), channelAccessToken);
        }

        public string ReplyMessage(string ReplyToken, Uri originalContentUrl, Uri previewImageUrl)
        {
            return Utility.ReplyImageMessage(ReplyToken, originalContentUrl.ToString(), previewImageUrl.ToString(), channelAccessToken);
        }

        private Bot GetBotInstance()
        {
            if (string.IsNullOrEmpty(ChannelAccessToken))
            {
                throw new Exception("ChannelAccessToken cannot be empty.");
            }

            return new Bot(ChannelAccessToken);
        }

        public byte[] GetUserUploadedContent(string ContentID)
        {
            return GetBotInstance().GetUserUploadedContent(ContentID);
        }

        public LineUserInfo GetUserInfo(string UserUid)
        {
            return GetBotInstance().GetUserInfo(UserUid);
        }

        public GroupSummary GetGroupSummary(string groupId)
        {
            return GetBotInstance().GetGroupSummary(groupId);
        }

        public int GetMembersInGroupCount(string groupId)
        {
            return GetBotInstance().GetMembersInGroupCount(groupId);
        }

        public int GetMembersInRoomCount(string roomId)
        {
            return GetBotInstance().GetMembersInRoomCount(roomId);
        }
    }
}
