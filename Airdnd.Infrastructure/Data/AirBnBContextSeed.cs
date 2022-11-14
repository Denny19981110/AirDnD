using Airdnd.Core.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static Airdnd.Core.Entities.UserAccount;
using Airdnd.Core.enums;
using Newtonsoft.Json;
using System.Security.Policy;
using System.IO;
using System.Threading.Channels;

namespace Airdnd.Infrastructure.Data
{
    public class AirBnBContextSeed
    {
        public static void SeedDevelopment(AirBnBContext context, ILogger logger, int retry = 0)
        {
            var retryForAvailability = retry;
            try
            {
                if (!context.Database.IsSqlServer())
                {
                    return;
                }

                //先刪除再建立，確保測試資料是乾淨的
                context.Database.EnsureDeleted();
                context.Database.EnsureCreated();
            }
            catch (Exception ex)
            {
                if (retryForAvailability >= 5) throw;

                retryForAvailability++;

                logger.LogError(ex.Message);
                SeedDevelopment(context, logger, retryForAvailability);
                throw;
            }
        }

        public static void SeedForProduction(AirBnBContext context, ILogger logger, int retry = 0)
        {
            var retryForAvailability = retry;
            try
            {
                if (!context.Database.IsSqlServer())
                {
                    return;
                }

                //套用Migrations相關紀錄
                context.Database.Migrate();
            }
            catch (Exception ex)
            {
                if (retryForAvailability >= 5) throw;

                retryForAvailability++;

                logger.LogError(ex.Message);
                SeedForProduction(context, logger, retryForAvailability);
                throw;
            }
        }
    }
    public static class SeedData
    {
        public static List<PropertyGroup> PropertyGroups()
        {
            return new List<PropertyGroup>
            {
                new PropertyGroup{PropertyGroupId = 1, PropertyGroupName = "公寓", ImagePath = "https://res.cloudinary.com/dziprrwvg/image/upload/v1664031229/%E5%85%AC%E5%AF%93_ybdrzs.jpg",IconPath="https://i.imgur.com/R6F4VFX.png"},
                new PropertyGroup{PropertyGroupId = 2, PropertyGroupName = "獨棟房屋", ImagePath = "https://res.cloudinary.com/dziprrwvg/image/upload/v1664031229/%E7%8D%A8%E6%A3%9F%E6%88%BF%E5%B1%8B_ed2p1c.jpg",IconPath="https://i.imgur.com/sRMPNMt.png"},
                new PropertyGroup{PropertyGroupId = 3, PropertyGroupName = "附屬建築", ImagePath = "https://res.cloudinary.com/dziprrwvg/image/upload/v1664031228/%E9%99%84%E5%B1%AC%E5%BB%BA%E7%AF%89_al5ksd.jpg",IconPath="https://i.imgur.com/jSKRfb1.png"},
                new PropertyGroup{PropertyGroupId = 4, PropertyGroupName = "獨特房源", ImagePath = "https://res.cloudinary.com/dziprrwvg/image/upload/v1664031228/%E7%8D%A8%E7%89%B9%E6%88%BF%E6%BA%90_uplvab.jpg",IconPath="https://i.imgur.com/sRMPNMt.png"},
                new PropertyGroup{PropertyGroupId = 5, PropertyGroupName = "家庭式旅館", ImagePath = "https://res.cloudinary.com/dziprrwvg/image/upload/v1664031228/%E5%AE%B6%E5%BA%AD%E5%BC%8F%E6%97%85%E9%A4%A8_yagqxz.jpg",IconPath="https://i.imgur.com/fyYyfUz.png"},
                new PropertyGroup{PropertyGroupId = 6, PropertyGroupName = "精品旅店", ImagePath = "https://res.cloudinary.com/dziprrwvg/image/upload/v1664031229/%E7%B2%BE%E5%93%81%E6%97%85%E5%BA%97_pw4nwd.jpg",IconPath="https://i.imgur.com/fyYyfUz.png"},
            };
        }
        public static List<PropertyType> PropertyTypes()
        {
            return new List<PropertyType>
            {
                new PropertyType{PropertyGroupId = 1, PropertyId = 1, PropertyName = "出租住所", PropertyContent = "集合住宅大樓或複合式建築中的出租屋。", IconPath = "。"},
                new PropertyType{PropertyGroupId = 1, PropertyId = 2, PropertyName = "私有公寓", PropertyContent = "位於集合住宅大樓或複合式建築的住處。", IconPath = ""},
                new PropertyType{PropertyGroupId = 1, PropertyId = 3, PropertyName = "Loft空間", PropertyContent = "開放式的公寓，內牆可能較矮。", IconPath = ""},
                new PropertyType{PropertyGroupId = 1, PropertyId = 4, PropertyName = "服務式公寓", PropertyContent = "由專業管理公司提供飯店管理服務的公寓。", IconPath = ""},
                new PropertyType{PropertyGroupId = 1, PropertyId = 5, PropertyName = "古巴式家庭旅館", PropertyContent = "古巴式家庭旅館內的獨立房間。",IconPath="https://a0.muscache.com/pictures/251c0635-cc91-4ef7-bb13-1084d5229446.jpg"},
                new PropertyType{PropertyGroupId = 1, PropertyId = 6, PropertyName = "度假屋", PropertyContent = "裝潢齊全的出租房源，包括廚房和浴室，且可會提供前台等客房服務。", IconPath = ""},
                new PropertyType{PropertyGroupId = 2, PropertyId = 7, PropertyName = "房源", PropertyContent = "獨棟房屋或與其他建築共用牆壁的房屋。", IconPath = ""},
                new PropertyType{PropertyGroupId = 2, PropertyId = 8, PropertyName = "小木屋", PropertyContent = "以木材等天然建材蓋成的房屋，且位於大自然中。",IconPath="https://a0.muscache.com/pictures/732edad8-3ae0-49a8-a451-29a8010dcc0c.jpg"},
                new PropertyType{PropertyGroupId = 2, PropertyId = 9, PropertyName = "別墅", PropertyContent = "豪華住宅，可能具有室內外空間、花園和泳池。", IconPath = ""},
                new PropertyType{PropertyGroupId = 2, PropertyId = 10, PropertyName = "連棟房屋", PropertyContent = "多樓層的連棟房屋或設有露台的房屋，彼此之間有共用牆壁，戶外空間也可能要共享。", IconPath = ""},
                new PropertyType{PropertyGroupId = 2, PropertyId = 11, PropertyName = "村舍", PropertyContent = "建於鄉間或湖水或海灘附近的溫馨房屋",IconPath="https://a0.muscache.com/pictures/6ad4bd95-f086-437d-97e3-14d12155ddfe.jpg"},
                new PropertyType{PropertyGroupId = 2, PropertyId = 12, PropertyName = "平房", PropertyContent = "具有寬敞的前門廊和傾斜屋頂的單層住宅。", IconPath = ""},
                new PropertyType{PropertyGroupId = 2, PropertyId = 13, PropertyName = "生態屋", PropertyContent = "建在地底下或以夯土等材料建成的房屋。", IconPath = ""},
                new PropertyType{PropertyGroupId = 2, PropertyId = 14, PropertyName = "船屋", PropertyContent = "水上的房屋，建造時是用陸上房屋相似的建材。",IconPath="https://a0.muscache.com/pictures/c027ff1a-b89c-4331-ae04-f8dee1cdc287.jpg"},
                new PropertyType{PropertyGroupId = 2, PropertyId = 15, PropertyName = "小屋", PropertyContent = "以木頭或泥土建成的房屋，屋頂可能是用茅草製成。", IconPath = ""},
                new PropertyType{PropertyGroupId = 2, PropertyId = 16, PropertyName = "農場住宿", PropertyContent = "位於鄉村地區的房屋，旅客可以體驗農村生活或與動物共度時光。", IconPath = ""},
                new PropertyType{PropertyGroupId = 2, PropertyId = 17, PropertyName = "圓頂屋", PropertyContent = "圓頂或球形房屋，例如氣泡屋。", IconPath = ""},
                new PropertyType{PropertyGroupId = 2, PropertyId = 18, PropertyName = "基克拉澤斯屋", PropertyContent = "位於希臘基克拉澤斯群島的白色坪頂房屋。",IconPath="https://a0.muscache.com/pictures/e4b12c1b-409b-4cb6-a674-7c1284449f6e.jpg"},
                new PropertyType{PropertyGroupId = 2, PropertyId = 19, PropertyName = "度假小木屋", PropertyContent = "帶斜屋頂的木屋，常常作為滑雪或夏日度假的住宿。", IconPath="https://a0.muscache.com/pictures/732edad8-3ae0-49a8-a451-29a8010dcc0c.jpg"},
                new PropertyType{PropertyGroupId = 2, PropertyId = 20, PropertyName = "義式傳統石屋", PropertyContent = "位於義大利潘泰萊里亞島的圓頂石造屋。",IconPath="https://a0.muscache.com/pictures/c9157d0a-98fe-4516-af81-44022118fbc7.jpg"},
                new PropertyType{PropertyGroupId = 2, PropertyId = 21, PropertyName = "燈塔", PropertyContent = "濱水的塔樓，配有強光可以指引船舶。", IconPath = ""},
                new PropertyType{PropertyGroupId = 2, PropertyId = 22, PropertyName = "牧人小屋", PropertyContent = "最初用於牧羊的小型四輪貨車。", IconPath = ""},
                new PropertyType{PropertyGroupId = 2, PropertyId = 23, PropertyName = "迷你屋", PropertyContent = "面積通常不到 11 坪的獨立房屋。",IconPath="https://a0.muscache.com/pictures/35919456-df89-4024-ad50-5fcb7a472df9.jpg"},
                new PropertyType{PropertyGroupId = 2, PropertyId = 24, PropertyName = "普利亞斗笠屋", PropertyContent = "擁有錐形屋頂的圓柱狀石造屋，源自義大利。",IconPath="https://a0.muscache.com/pictures/33848f9e-8dd6-4777-b905-ed38342bacb9.jpg"},
                new PropertyType{PropertyGroupId = 2, PropertyId = 25, PropertyName = "古巴式家庭旅館", PropertyContent = "帶有燒烤和公共空間的韓國鄉村房屋。", IconPath="https://a0.muscache.com/pictures/251c0635-cc91-4ef7-bb13-1084d5229446.jpg"},
                new PropertyType{PropertyGroupId = 2, PropertyId = 26, PropertyName = "韓國膳宿公寓", PropertyContent = "帶有燒烤和公共空間的韓國鄉村房屋。", IconPath = ""},
                new PropertyType{PropertyGroupId = 2, PropertyId = 27, PropertyName = "度假屋", PropertyContent = "裝潢齊全的出租房源，包括廚房和浴室，且可能會提供前台等客房服務。", IconPath = ""},
                new PropertyType{PropertyGroupId = 3, PropertyId = 28, PropertyName = "客用住房", PropertyContent = "與屋主分開的獨立建築。", IconPath = ""},
                new PropertyType{PropertyGroupId = 3, PropertyId = 29, PropertyName = "客用套房", PropertyContent = "擁有獨立入口的房源，位於較大建築物或與之相聯。", IconPath = ""},
                new PropertyType{PropertyGroupId = 3, PropertyId = 30, PropertyName = "農場住宿", PropertyContent = "位於鄉村地區的房屋，旅客可以體驗農村生活或與動物共度時光。", IconPath = ""},
                new PropertyType{PropertyGroupId = 3, PropertyId = 31, PropertyName = "度假屋", PropertyContent = "裝潢齊全的出租房源，包括廚房和浴室，且可能會提供前台等客房服務。", IconPath = ""},
                new PropertyType{PropertyGroupId = 4, PropertyId = 32, PropertyName = "穀倉", PropertyContent = "用作存放穀物、農作器具或四養生醋的改造住宅空間。",IconPath="https://a0.muscache.com/pictures/f60700bc-8ab5-424c-912b-6ef17abc479a.jpg"},
                new PropertyType{PropertyGroupId = 4, PropertyId = 33, PropertyName = "船", PropertyContent = "在房客入住其靠岸停泊的船隻、帆船或遊艇，但不是船屋。",IconPath="https://a0.muscache.com/pictures/687a8682-68b3-4f21-8d71-3c3aef6c1110.jpg"},
                new PropertyType{PropertyGroupId = 4, PropertyId = 34, PropertyName = "巴士", PropertyContent = "經過改造的大型客車，內部裝潢充滿創意巧思。", IconPath = ""},
                new PropertyType{PropertyGroupId = 4, PropertyId = 35, PropertyName = "露營車/休旅車", PropertyContent = "車上住宿空間或露營拖車，是住家兼交通工具。",IconPath="https://a0.muscache.com/pictures/31c1d523-cc46-45b3-957a-da76c30c85f9.jpg"},
                new PropertyType{PropertyGroupId = 4, PropertyId = 36, PropertyName = "樹屋", PropertyContent = "建於樹幹之中、樹枝上，或位於樹林中蓋在樹根上的住所。",IconPath="https://a0.muscache.com/pictures/4d4a4eba-c7e4-43eb-9ce2-95e1d200d10e.jpg"},
                new PropertyType{PropertyGroupId = 4, PropertyId = 37, PropertyName = "營地", PropertyContent = "可供房客搭設帳篷、蒙古包、露營車或迷你屋的場地。",IconPath="https://a0.muscache.com/pictures/ca25c7f3-0d1f-432b-9efa-b9f5dc6d8770.jpg"},
                new PropertyType{PropertyGroupId = 4, PropertyId = 38, PropertyName = "城堡", PropertyContent = "可能具有歷史意義的宏偉建築，也許設有塔樓和護城河。",IconPath="https://a0.muscache.com/pictures/1b6a8b70-a3b6-48b5-88e1-2243d9172c06.jpg"},
                new PropertyType{PropertyGroupId = 4, PropertyId = 39, PropertyName = "洞穴", PropertyContent = "位於山坡或懸崖上，自然生成或人為開挖出來的住所。",IconPath="https://a0.muscache.com/pictures/4221e293-4770-4ea8-a4fa-9972158d4004.jpg"},
                new PropertyType{PropertyGroupId = 4, PropertyId = 40, PropertyName = "圓頂屋", PropertyContent = "圓頂或球形房屋，例如氣泡屋。", IconPath = ""},
                new PropertyType{PropertyGroupId = 4, PropertyId = 41, PropertyName = "生態屋", PropertyContent = "建在地底下或以夯土等材料建成的房屋。",IconPath="https://a0.muscache.com/pictures/d7445031-62c4-46d0-91c3-4f29f9790f7a.jpg"},
                new PropertyType{PropertyGroupId = 4, PropertyId = 42, PropertyName = "農場住宿", PropertyContent = "位於鄉村地區的房屋，旅客可以體驗農村生活或與動物共度時光。",IconPath="https://a0.muscache.com/pictures/aaa02c2d-9f0d-4c41-878a-68c12ec6c6bd.jpg"},
                new PropertyType{PropertyGroupId = 4, PropertyId = 43, PropertyName = "船屋", PropertyContent = "水上的房屋，建造時是用陸上房屋相似的建材。", IconPath="https://a0.muscache.com/pictures/c027ff1a-b89c-4331-ae04-f8dee1cdc287.jpg"},
                new PropertyType{PropertyGroupId = 4, PropertyId = 44, PropertyName = "小屋", PropertyContent = "以木頭或泥土建成的房屋，屋頂可能是用茅草製成。", IconPath = ""},
                new PropertyType{PropertyGroupId = 4, PropertyId = 45, PropertyName = "冰製圓頂屋", PropertyContent = "以冰雪建成的圓頂建築，常見於寒冷地區。" ,IconPath="https://a0.muscache.com/pictures/89faf9ae-bbbc-4bc4-aecd-cc15bf36cbca.jpg"},
                new PropertyType{PropertyGroupId = 4, PropertyId = 46, PropertyName = "島嶼", PropertyContent = "一片四面環水的土地。",IconPath="https://a0.muscache.com/pictures/8e507f16-4943-4be9-b707-59bd38d56309.jpg"},
                new PropertyType{PropertyGroupId = 4, PropertyId = 47, PropertyName = "燈塔", PropertyContent = "濱水的塔樓，配有強光可以指引船舶。", IconPath = ""},
                new PropertyType{PropertyGroupId = 4, PropertyId = 48, PropertyName = "飛機", PropertyContent = "由飛機改造而成的住宿。", IconPath = ""},
                new PropertyType{PropertyGroupId = 4, PropertyId = 49, PropertyName = "牧場", PropertyContent = "位於大片畜牧土地上的住宿。", IconPath = ""},
                new PropertyType{PropertyGroupId = 4, PropertyId = 50, PropertyName = "宗教建築", PropertyContent = "由教堂或清真寺等禮拜場所改造而成的空間。", IconPath = ""},
                new PropertyType{PropertyGroupId = 4, PropertyId = 51, PropertyName = "牧人小屋", PropertyContent = "最初用於牧羊的小型四輪貨車。",IconPath="https://a0.muscache.com/pictures/747b326c-cb8f-41cf-a7f9-809ab646e10c.jpg"},
                new PropertyType{PropertyGroupId = 4, PropertyId = 52, PropertyName = "貨櫃屋", PropertyContent = "由貨運鋼製貨櫃改造而成的房源。",IconPath="https://a0.muscache.com/pictures/0ff9740e-52a2-4cd5-ae5a-94e1bfb560d6.jpg"},
                new PropertyType{PropertyGroupId = 4, PropertyId = 53, PropertyName = "帳篷", PropertyContent = "以布料和桿子搭建而成的結構，通常可折疊移動。", IconPath = ""},
                new PropertyType{PropertyGroupId = 4, PropertyId = 54, PropertyName = "米你屋", PropertyContent = "面積通常不到 11 坪的獨立房屋。", IconPath = ""},
                new PropertyType{PropertyGroupId = 4, PropertyId = 55, PropertyName = "印地安帳篷", PropertyContent = "以桿子之稱的錐形帳篷，帶掀開式的門與開放式的頂部。", IconPath = ""},
                new PropertyType{PropertyGroupId = 4, PropertyId = 56, PropertyName = "塔樓", PropertyContent = "層數很多且可欣賞風景的獨立式結構。",IconPath="https://a0.muscache.com/pictures/d721318f-4752-417d-b4fa-77da3cbc3269.jpg"},
                new PropertyType{PropertyGroupId = 4, PropertyId = 57, PropertyName = "火車", PropertyContent = "由守車、篷車和其他有軌車輛改造而成的居住空間。", IconPath = ""},
                new PropertyType{PropertyGroupId = 4, PropertyId = 58, PropertyName = "風車", PropertyContent = "在用來風力發電的建築中，可供居住的起居空間。",IconPath="https://a0.muscache.com/pictures/5cdb8451-8f75-4c5f-a17d-33ee228e3db8.jpg"},
                new PropertyType{PropertyGroupId = 4, PropertyId = 59, PropertyName = "蒙古包", PropertyContent = "。",IconPath="https://a0.muscache.com/pictures/4759a0a7-96a8-4dcd-9490-ed785af6df14.jpg"},
                new PropertyType{PropertyGroupId = 4, PropertyId = 60, PropertyName = "Riad", PropertyContent = "設有露天庭院或花園的摩洛哥傳統房屋。", IconPath = ""},
                new PropertyType{PropertyGroupId = 4, PropertyId = 61, PropertyName = "韓國膳宿公寓", PropertyContent = "帶有燒烤和公共空間的韓國鄉村房屋。", IconPath = ""},
                new PropertyType{PropertyGroupId = 4, PropertyId = 62, PropertyName = "度假屋", PropertyContent = "裝潢齊全的出租房源，包括廚房和浴室，且可會提供前台等客房服務。", IconPath = ""},
                new PropertyType{PropertyGroupId = 4, PropertyId = 63, PropertyName = "其他", PropertyContent = "", IconPath = ""},
                new PropertyType{PropertyGroupId = 5, PropertyId = 64, PropertyName = "家庭式旅館", PropertyContent = "為房客提供早餐，且有房東在現場專業管理民宿。", IconPath = ""},
                new PropertyType{PropertyGroupId = 5, PropertyId = 65, PropertyName = "自然小屋", PropertyContent = "靠近森林、山間等自然環境的專業管理住宿。", IconPath = ""},
                new PropertyType{PropertyGroupId = 5, PropertyId = 66, PropertyName = "農場住宿", PropertyContent = "位於鄉村地區的房屋，旅客可以體驗農村生活或與動物共度時光。", IconPath = ""},
                new PropertyType{PropertyGroupId = 5, PropertyId = 67, PropertyName = "台灣民宿", PropertyContent = "位房客提供獨立房間的台灣專業管理住宿。", IconPath = ""},
                new PropertyType{PropertyGroupId = 5, PropertyId = 68, PropertyName = "古巴式家庭旅館", PropertyContent = "帶有燒烤和公共空間的韓國鄉村房屋。", IconPath="https://a0.muscache.com/pictures/251c0635-cc91-4ef7-bb13-1084d5229446.jpg"},
                new PropertyType{PropertyGroupId = 5, PropertyId = 69, PropertyName = "日式旅館", PropertyContent = "為房客提供獨特文化體驗的日本小旅館。", IconPath = ""},
                new PropertyType{PropertyGroupId = 6, PropertyId = 70, PropertyName = "飯店", PropertyContent = "位房客提供獨立房間、套房或獨特房源的專業管理住宿。", IconPath = ""},
                new PropertyType{PropertyGroupId = 6, PropertyId = 71, PropertyName = "青年旅舍", PropertyContent = "出租和住房間床位和獨立房間的專業管理住宿。", IconPath = ""},
                new PropertyType{PropertyGroupId = 6, PropertyId = 72, PropertyName = "渡假村", PropertyContent = "裝潢齊全的出租房源，包括廚房和浴室，且可會提供前台等客房服務。", IconPath = ""},
                new PropertyType{PropertyGroupId = 6, PropertyId = 73, PropertyName = "自然小屋", PropertyContent = "靠近森林、山間等自然環境的專業管理住宿。", IconPath = ""},
                new PropertyType{PropertyGroupId = 6, PropertyId = 74, PropertyName = "精品旅店", PropertyContent = "具有獨特風格或裝潢主題的專業管理民宿。", IconPath = ""},
                new PropertyType{PropertyGroupId = 6, PropertyId = 75, PropertyName = "公寓式旅店", PropertyContent = "提供飯店式服務與房間的公寓式住宿。", IconPath = ""},
                new PropertyType{PropertyGroupId = 6, PropertyId = 76, PropertyName = "服務式公寓", PropertyContent = "由專業管理公司提供飯店式管理服務公寓。", IconPath = ""},
                new PropertyType{PropertyGroupId = 6, PropertyId = 77, PropertyName = "文化遺產旅店", PropertyContent = "由歷史建築改造而成的印度住宿。", IconPath = ""},
                new PropertyType{PropertyGroupId = 6, PropertyId = 78, PropertyName = "客棧", PropertyContent = "具有當地特色和完善設施的中國住宿。", IconPath = ""},
            };
        }
        public static List<PrivacyType> PrivacyTypes()
        {
            return new List<PrivacyType>
            {
                new PrivacyType{PrivacyTypeId = 1, PrivacyTypeName = "獨立房間", PrivacyTypeContent = "房源或飯店內的獨立客房，以及部分共用空間"},
                new PrivacyType{PrivacyTypeId = 2, PrivacyTypeName = "合住房間", PrivacyTypeContent = "可能要和他人共用的就寢空間和公共區域"},
                new PrivacyType{PrivacyTypeId = 3, PrivacyTypeName = "整套房源", PrivacyTypeContent = "獨享整間房源"},
            };
        }
        public static List<ServiceType> ServiceTypes()
        {
            return new List<ServiceType>
            {
                new ServiceType{ServiceTypeId = 1, ServiceTypeName = "房源是否設有任何獨特的設備與服務？"},
                new ServiceType{ServiceTypeId = 2, ServiceTypeName = "這些最受旅客歡迎的設備與服務呢？"},
                new ServiceType{ServiceTypeId = 3, ServiceTypeName = "是否備有以下保安設備？"},
            };
        }
        public static List<Service> Services()
        {
            return new List<Service>
            {
                //<span class="material-symbols-outlined">變數</span>
                new Service{ServiceTypeId = 1, ServiceId = 1, ServiceName = "游泳池", IconPath = "waves", Sort = 1},
                new Service{ServiceTypeId = 1, ServiceId = 2, ServiceName = "按摩浴池", IconPath = "spa", Sort = 2},
                new Service{ServiceTypeId = 1, ServiceId = 3, ServiceName = "庭院", IconPath = "outdoor_garden", Sort = 3},
                new Service{ServiceTypeId = 1, ServiceId = 4, ServiceName = "烤肉區", IconPath = "outdoor_grill", Sort = 4},
                new Service{ServiceTypeId = 1, ServiceId = 5, ServiceName = "火坑", IconPath = "local_fire_department", Sort = 5},
                new Service{ServiceTypeId = 1, ServiceId = 6, ServiceName = "撞球桌", IconPath = "sports_handball", Sort = 6},
                new Service{ServiceTypeId = 1, ServiceId = 7, ServiceName = "室內壁爐", IconPath = "fireplace", Sort = 7},
                new Service{ServiceTypeId = 1, ServiceId = 8, ServiceName = "戶外用餐區", IconPath = "kebab_dining", Sort = 8},
                new Service{ServiceTypeId = 1, ServiceId = 9, ServiceName = "運動器材", IconPath = "sports_tennis", Sort = 9},
                new Service{ServiceTypeId = 2, ServiceId = 10, ServiceName = "Wifi", IconPath = "rss_feed", Sort = 1},
                new Service{ServiceTypeId = 2, ServiceId = 11, ServiceName = "電視", IconPath = "tv", Sort = 2},
                new Service{ServiceTypeId = 2, ServiceId = 12, ServiceName = "廚房", IconPath = "kitchen", Sort = 3},
                new Service{ServiceTypeId = 2, ServiceId = 13, ServiceName = "洗衣機", IconPath = "soap", Sort = 4},
                new Service{ServiceTypeId = 2, ServiceId = 14, ServiceName = "室內免費停車", IconPath = "local_parking", Sort = 5},
                new Service{ServiceTypeId = 2, ServiceId = 15, ServiceName = "室內收費停車", IconPath = "garage", Sort = 6},
                new Service{ServiceTypeId = 2, ServiceId = 16, ServiceName = "空調設備", IconPath = "air", Sort = 7},
                new Service{ServiceTypeId = 2, ServiceId = 17, ServiceName = "工作空間", IconPath = "work", Sort = 8},
                new Service{ServiceTypeId = 2, ServiceId = 18, ServiceName = "戶外淋浴空間", IconPath = "shower", Sort = 9},
                new Service{ServiceTypeId = 3, ServiceId = 19, ServiceName = "煙霧警報器", IconPath = "detector_smoke", Sort = 1},
                new Service{ServiceTypeId = 3, ServiceId = 20, ServiceName = "急救包", IconPath = "medical_services", Sort = 2},
                new Service{ServiceTypeId = 3, ServiceId = 21, ServiceName = "一氧化碳警報器", IconPath = "detector_alarm", Sort = 3},
                new Service{ServiceTypeId = 3, ServiceId = 22, ServiceName = "滅火器", IconPath = "fire_extinguisher", Sort = 4},
            };
        }
        public static List<Legal> Legals()
        {
            return new List<Legal>
            {
                new Legal{LegalId = 1, LegalName = "監視錄影器",Sort = 1},
                new Legal{LegalId = 2, LegalName = "武器",Sort = 2},
                new Legal{LegalId = 3, LegalName = "危險動物",Sort = 3},
            };
        }
        public static List<Highlight> Highlights()
        {
            return new List<Highlight>
            {
                new Highlight{HighlightId = 1, HighlightName = "搶手", IconPath ="paid", Sort = 1},
                new Highlight{HighlightId = 2, HighlightName = "鄉村", IconPath ="emoji_nature", Sort = 2},
                new Highlight{HighlightId = 3, HighlightName = "擁抱大自然", IconPath ="nature_people", Sort = 3},
                new Highlight{HighlightId = 4, HighlightName = "令人難忘", IconPath ="sentiment_very_satisfied", Sort = 4},
                new Highlight{HighlightId = 5, HighlightName = "浪漫", IconPath ="volunteer_activism", Sort = 5},
                new Highlight{HighlightId = 6, HighlightName = "歷史悠久", IconPath ="public", Sort = 6},
            };
        }
        public static List<LegalListing> Legallights()
        {
            return new List<LegalListing>
            {
                new LegalListing{LegalId=1,ListingId=1,LlegalId=1},
                new LegalListing{LegalId=1,ListingId=2,LlegalId=2},
                new LegalListing{LegalId=1,ListingId=3,LlegalId=3},
                new LegalListing{LegalId=1,ListingId=8,LlegalId=4},
                new LegalListing{LegalId=1,ListingId=9,LlegalId=5},
            };
        }
        public static List<UserAccount> UserAccounts()
        {
            string jsonFilePath = @"wwwroot/assert/persons.json";
            string Json = File.ReadAllText(jsonFilePath);
            List<UserAccount> roadInfos = JsonConvert.DeserializeObject<List<UserAccount>>(Json);
            return roadInfos;
            //目前UserAccounts的ID:1-6號是房東
            //目前UserAccounts的ID:7-21號是房客
        }
        public static List<WishList> WishLists()
        {
            return new List<WishList>
            {
                new WishList{WishlistId=1,UserAccountId=1,WishGroupName="星塵鬥士"},
                new WishList{WishlistId=2,UserAccountId=1,WishGroupName="不滅鑽石"},
                new WishList{WishlistId=3,UserAccountId=2,WishGroupName="黃金之風"},
                new WishList{WishlistId=4,UserAccountId=4,WishGroupName="石之海"},
            };
        }

        public static List<WishListDetail> WishListDetails()
        {
            return new List<WishListDetail>
            {
                new WishListDetail{WishlistDetailId=1,WishlistId=1,ListingId=1,CreatTime=DateTime.UtcNow},
                new WishListDetail{WishlistDetailId=2,WishlistId=1,ListingId=2,CreatTime=DateTime.UtcNow},
                new WishListDetail{WishlistDetailId=3,WishlistId=1,ListingId=3,CreatTime=DateTime.UtcNow},
            };
        }

        public static List<Listing> Listings()
        {
            string Json = LoadJson(@"wwwroot/assert/listing.json");
            List<Listing> loadList = JsonConvert.DeserializeObject<List<Listing>>(Json);
            return loadList;
            //return new List<Listing>
            //{
            //    new Listing{ListingId=1,DefaultPrice=2000,HighlightId=1,Address="澎湖縣馬公市西衛里246-1號",ListingName="星空鯨魚",Description="Starrywhale hostel 位於馬公市，設有共用休息室和 WiFi（免費），距離國立澎湖科技大學不到 1 公里，距離南海遊客中心 2.4 公里。",PropertyId=3,CategoryId=1,Expected=5,UserAccountId=5,Status=StatusType.HasUpload,Bed=2,BedRoom=1,BathRoom=1,Toilet=1,CreateTime=new DateTime(2022,9,17),IndieBathroom=true,Lat=23.5854477,Lng=119.5779188},
            //    new Listing{ListingId=2,DefaultPrice=7000,HighlightId=4,Address="澎湖縣白沙鄉中屯106號 ",ListingName="夏日澄藍",Description="The Charming Land 距離澎湖水族館 5 公里。最近的機場是 Wang-an，距離 The Charming Land 15 公里。住宿提供免費機場接駁服務。",PropertyId=2,CategoryId=1,Expected=3,UserAccountId=4,Status=StatusType.HasUpload,Bed=2,BedRoom=1,BathRoom=1,Toilet=1,CreateTime=new DateTime(2021,11,10),IndieBathroom=true,Lat=23.6158429,Lng=119.6018986},
            //    new Listing{ListingId=3,DefaultPrice=2200,HighlightId=6,Address="澎湖縣馬公市嵵裡里109之10號 ",ListingName="澎湖香亭",Description="民宿距離風櫃洞約 6 分鐘車程，距離波賽頓海洋運動俱樂部步行不到 10 分鐘，距離澎坊免稅商店不到 15 分鐘車程，距離海灘約 100 公尺。",PropertyId=3,CategoryId=1,Expected=2,UserAccountId=3,Status=StatusType.HasUpload,Bed=2,BedRoom=1,BathRoom=1,Toilet=1,CreateTime=new DateTime(2021,2,1),IndieBathroom=true,Lat=23.5267125,Lng=119.5672227},
            //    new Listing{ListingId=4,DefaultPrice=1900,HighlightId=3,Address="澎湖縣西嶼鄉合界村後螺3-2號",ListingName="23.5蔚藍民宿",Description="有拜有保庇",PropertyId=3,CategoryId=1,Expected=5,UserAccountId=2,Status=StatusType.HasUpload,Bed=2,BedRoom=1,BathRoom=1,Toilet=1,CreateTime=new DateTime(2018,6,15),IndieBathroom=true,Lat=23.6373023,Lng=119.5328935},
            //    new Listing{ListingId=5,DefaultPrice=3500,HighlightId=2,Address="澎湖縣山水里珠江110~27號",ListingName="圓頂瓦舍海景民宿",Description="圓頂瓦舍海景民宿正前方50公尺就是山水沙灘，以及前來戲沙玩水的遊客們的尖叫嬉笑聲。",PropertyId=1,CategoryId=1,Expected=5,UserAccountId=2,Status=StatusType.HasUpload,Bed=2,BedRoom=1,BathRoom=1,Toilet=1,CreateTime=new DateTime(2022,9,17),IndieBathroom=true,Lat=23.5151156,Lng=119.5887878},
            //    new Listing{ListingId=6,DefaultPrice=1800,HighlightId=1,Address="澎湖縣七美鄉南港村35號",ListingName="幸福小棧",Description="下船後走路就會到，獨立小屋，提供您到訪七美一個休憩的好地方",PropertyId=2,CategoryId=1,Expected=5,UserAccountId=1,Status=StatusType.HasUpload,Bed=2,BedRoom=1,BathRoom=1,Toilet=1,CreateTime=new DateTime(2021,7,17),IndieBathroom=true,Lat=23.1963654,Lng=119.4220347},
            //};
        }
        public static List<ServiceListing> service()
        {
            string Json = LoadJson(@"wwwroot/assert/service.json");
            List<ServiceListing> loadService = JsonConvert.DeserializeObject<List<ServiceListing>>(Json);
            return loadService;
        }
            private static string LoadJson(string FilePath)
        {
            string jsonFilePath = FilePath;
            string Json = File.ReadAllText(jsonFilePath);
            return Json;
        }

        public static List<ListingImage> ListingsImg()
        {
            return new List<ListingImage>
            {
               new ListingImage{ListingImageId=1,ListingId=1,ListingImagePath="https://kamiiliu.github.io/houseImg/HousePic001.webp"},
               new ListingImage{ListingImageId=2,ListingId=1,ListingImagePath="https://kamiiliu.github.io/houseImg/HousePic002.webp"},
               new ListingImage{ListingImageId=3,ListingId=1,ListingImagePath="https://kamiiliu.github.io/houseImg/HousePic003.webp"},
               new ListingImage{ListingImageId=4,ListingId=1,ListingImagePath="https://kamiiliu.github.io/houseImg/HousePic004.webp"},
               new ListingImage{ListingImageId=5,ListingId=1,ListingImagePath="https://kamiiliu.github.io/houseImg/HousePic005.webp"},
               new ListingImage{ListingImageId=6,ListingId=2,ListingImagePath="https://kamiiliu.github.io/houseImg/HousePic006.webp"},
               new ListingImage{ListingImageId=7,ListingId=2,ListingImagePath="https://kamiiliu.github.io/houseImg/HousePic007.webp"},
               new ListingImage{ListingImageId=8,ListingId=2,ListingImagePath="https://kamiiliu.github.io/houseImg/HousePic008.webp"},
               new ListingImage{ListingImageId=9,ListingId=2,ListingImagePath="https://kamiiliu.github.io/houseImg/HousePic009.webp"},
               new ListingImage{ListingImageId=10,ListingId=2,ListingImagePath="https://kamiiliu.github.io/houseImg/HousePic010.webp"},
               new ListingImage{ListingImageId=11,ListingId=3,ListingImagePath="https://kamiiliu.github.io/houseImg/HousePic011.webp"},
               new ListingImage{ListingImageId=12,ListingId=3,ListingImagePath="https://kamiiliu.github.io/houseImg/HousePic012.webp"},
               new ListingImage{ListingImageId=13,ListingId=3,ListingImagePath="https://kamiiliu.github.io/houseImg/HousePic013.webp"},
               new ListingImage{ListingImageId=14,ListingId=3,ListingImagePath="https://kamiiliu.github.io/houseImg/HousePic014.webp"},
               new ListingImage{ListingImageId=15,ListingId=3,ListingImagePath="https://kamiiliu.github.io/houseImg/HousePic015.webp"},
               new ListingImage{ListingImageId=16,ListingId=4,ListingImagePath="https://kamiiliu.github.io/houseImg/HousePic016.webp"},
               new ListingImage{ListingImageId=17,ListingId=4,ListingImagePath="https://kamiiliu.github.io/houseImg/HousePic017.webp"},
               new ListingImage{ListingImageId=18,ListingId=4,ListingImagePath="https://kamiiliu.github.io/houseImg/HousePic018.webp"},
               new ListingImage{ListingImageId=19,ListingId=4,ListingImagePath="https://kamiiliu.github.io/houseImg/HousePic019.webp"},
               new ListingImage{ListingImageId=20,ListingId=4,ListingImagePath="https://kamiiliu.github.io/houseImg/HousePic020.webp"},
               new ListingImage{ListingImageId=21,ListingId=5,ListingImagePath="https://kamiiliu.github.io/houseImg/HousePic021.webp"},
               new ListingImage{ListingImageId=22,ListingId=5,ListingImagePath="https://kamiiliu.github.io/houseImg/HousePic022.webp"},
               new ListingImage{ListingImageId=23,ListingId=5,ListingImagePath="https://kamiiliu.github.io/houseImg/HousePic023.webp"},
               new ListingImage{ListingImageId=24,ListingId=5,ListingImagePath="https://kamiiliu.github.io/houseImg/HousePic024.webp"},
               new ListingImage{ListingImageId=25,ListingId=5,ListingImagePath="https://kamiiliu.github.io/houseImg/HousePic025.webp"},
               new ListingImage{ListingImageId=26,ListingId=6,ListingImagePath="https://kamiiliu.github.io/houseImg/HousePic026.webp"},
               new ListingImage{ListingImageId=27,ListingId=6,ListingImagePath="https://kamiiliu.github.io/houseImg/HousePic027.webp"},
               new ListingImage{ListingImageId=28,ListingId=6,ListingImagePath="https://kamiiliu.github.io/houseImg/HousePic028.webp"},
               new ListingImage{ListingImageId=29,ListingId=6,ListingImagePath="https://kamiiliu.github.io/houseImg/HousePic029.webp"},
               new ListingImage{ListingImageId=30,ListingId=6,ListingImagePath="https://kamiiliu.github.io/houseImg/HousePic030.webp"},
               new ListingImage{ListingImageId=31,ListingId=7,ListingImagePath="https://kamiiliu.github.io/houseImg/HousePic031.webp"},
               new ListingImage{ListingImageId=32,ListingId=7,ListingImagePath="https://kamiiliu.github.io/houseImg/HousePic032.webp"},
               new ListingImage{ListingImageId=33,ListingId=7,ListingImagePath="https://kamiiliu.github.io/houseImg/HousePic033.webp"},
               new ListingImage{ListingImageId=34,ListingId=7,ListingImagePath="https://kamiiliu.github.io/houseImg/HousePic034.webp"},
               new ListingImage{ListingImageId=35,ListingId=7,ListingImagePath="https://kamiiliu.github.io/houseImg/HousePic035.webp"},
               new ListingImage{ListingImageId=36,ListingId=8,ListingImagePath="https://kamiiliu.github.io/houseImg/HousePic036.webp"},
               new ListingImage{ListingImageId=37,ListingId=8,ListingImagePath="https://kamiiliu.github.io/houseImg/HousePic037.webp"},
               new ListingImage{ListingImageId=38,ListingId=8,ListingImagePath="https://kamiiliu.github.io/houseImg/HousePic038.webp"},
               new ListingImage{ListingImageId=39,ListingId=8,ListingImagePath="https://kamiiliu.github.io/houseImg/HousePic039.webp"},
               new ListingImage{ListingImageId=40,ListingId=8,ListingImagePath="https://kamiiliu.github.io/houseImg/HousePic040.webp"},
               new ListingImage{ListingImageId=41,ListingId=9,ListingImagePath="https://kamiiliu.github.io/houseImg/HousePic041.webp"},
               new ListingImage{ListingImageId=42,ListingId=9,ListingImagePath="https://kamiiliu.github.io/houseImg/HousePic042.webp"},
               new ListingImage{ListingImageId=43,ListingId=9,ListingImagePath="https://kamiiliu.github.io/houseImg/HousePic043.webp"},
               new ListingImage{ListingImageId=44,ListingId=9,ListingImagePath="https://kamiiliu.github.io/houseImg/HousePic044.webp"},
               new ListingImage{ListingImageId=45,ListingId=9,ListingImagePath="https://kamiiliu.github.io/houseImg/HousePic045.webp"},
               new ListingImage{ListingImageId=46,ListingId=11,ListingImagePath="https://kamiiliu.github.io/houseImg/HousePic046.webp"},
               new ListingImage{ListingImageId=47,ListingId=11,ListingImagePath="https://kamiiliu.github.io/houseImg/HousePic047.webp"},
               new ListingImage{ListingImageId=48,ListingId=11,ListingImagePath="https://kamiiliu.github.io/houseImg/HousePic048.webp"},
               new ListingImage{ListingImageId=49,ListingId=11,ListingImagePath="https://kamiiliu.github.io/houseImg/HousePic049.webp"},
               new ListingImage{ListingImageId=50,ListingId=11,ListingImagePath="https://kamiiliu.github.io/houseImg/HousePic050.webp"},
               new ListingImage{ListingImageId=51,ListingId=10,ListingImagePath="https://kamiiliu.github.io/houseImg/HousePic051.webp"},
               new ListingImage{ListingImageId=52,ListingId=10,ListingImagePath="https://kamiiliu.github.io/houseImg/HousePic052.webp"},
               new ListingImage{ListingImageId=53,ListingId=10,ListingImagePath="https://kamiiliu.github.io/houseImg/HousePic053.webp"},
               new ListingImage{ListingImageId=54,ListingId=10,ListingImagePath="https://kamiiliu.github.io/houseImg/HousePic054.webp"},
               new ListingImage{ListingImageId=55,ListingId=10,ListingImagePath="https://kamiiliu.github.io/houseImg/HousePic055.webp"},
               new ListingImage{ListingImageId=56,ListingId=12,ListingImagePath="https://kamiiliu.github.io/houseImg/HousePic056.webp"},
               new ListingImage{ListingImageId=57,ListingId=12,ListingImagePath="https://kamiiliu.github.io/houseImg/HousePic057.webp"},
               new ListingImage{ListingImageId=58,ListingId=12,ListingImagePath="https://kamiiliu.github.io/houseImg/HousePic058.webp"},
               new ListingImage{ListingImageId=59,ListingId=12,ListingImagePath="https://kamiiliu.github.io/houseImg/HousePic059.webp"},
               new ListingImage{ListingImageId=60,ListingId=12,ListingImagePath="https://kamiiliu.github.io/houseImg/HousePic060.webp"},
               new ListingImage{ListingImageId=61,ListingId=13,ListingImagePath="https://kamiiliu.github.io/houseImg/HousePic061.webp"},
               new ListingImage{ListingImageId=62,ListingId=13,ListingImagePath="https://kamiiliu.github.io/houseImg/HousePic062.webp"},
               new ListingImage{ListingImageId=63,ListingId=13,ListingImagePath="https://kamiiliu.github.io/houseImg/HousePic063.webp"},
               new ListingImage{ListingImageId=64,ListingId=13,ListingImagePath="https://kamiiliu.github.io/houseImg/HousePic064.webp"},
               new ListingImage{ListingImageId=65,ListingId=13,ListingImagePath="https://kamiiliu.github.io/houseImg/HousePic065.webp"},
               new ListingImage{ListingImageId=66,ListingId=14,ListingImagePath="https://kamiiliu.github.io/houseImg/HousePic066.webp"},
               new ListingImage{ListingImageId=67,ListingId=14,ListingImagePath="https://kamiiliu.github.io/houseImg/HousePic067.webp"},
               new ListingImage{ListingImageId=68,ListingId=14,ListingImagePath="https://kamiiliu.github.io/houseImg/HousePic068.webp"},
               new ListingImage{ListingImageId=69,ListingId=14,ListingImagePath="https://kamiiliu.github.io/houseImg/HousePic069.webp"},
               new ListingImage{ListingImageId=70,ListingId=14,ListingImagePath="https://kamiiliu.github.io/houseImg/HousePic070.webp"},
               new ListingImage{ListingImageId=71,ListingId=15,ListingImagePath="https://kamiiliu.github.io/houseImg/HousePic071.webp"},
               new ListingImage{ListingImageId=72,ListingId=15,ListingImagePath="https://kamiiliu.github.io/houseImg/HousePic072.webp"},
               new ListingImage{ListingImageId=73,ListingId=15,ListingImagePath="https://kamiiliu.github.io/houseImg/HousePic073.webp"},
               new ListingImage{ListingImageId=74,ListingId=15,ListingImagePath="https://kamiiliu.github.io/houseImg/HousePic074.webp"},
               new ListingImage{ListingImageId=75,ListingId=15,ListingImagePath="https://kamiiliu.github.io/houseImg/HousePic075.webp"},
            };
        }

        //是否有監視器等等的表單
        public static List<LegalListing> LegalListings()
        {
            return new List<LegalListing>
            {
                new LegalListing{LlegalId=1,LegalId=1,ListingId=1},
                new LegalListing{LlegalId=2,LegalId=1,ListingId=1},
                new LegalListing{LlegalId=3,LegalId=1,ListingId=1},
                new LegalListing{LlegalId=4,LegalId=1,ListingId=1},
                new LegalListing{LlegalId=5,LegalId=1,ListingId=1},
            };
        }
        //public static List<Order> Orders()
        //{
        //    return new List<Order>
        //    {
        //        new Order{OrderId=1,CustomerId=11,ListingId=1,CreateDate= new DateTime(2022,8,10)
        //        ,CheckInDate = new DateTime(2022,8,10)
        //        ,FinishDate = new DateTime(2022,8,12)
        //        ,StayNight=2,PaymentType=1,Adults=2,Children=0,Infants=0,UnitPrice=1990,Status=2,TranStatus=1},
        //        new Order{OrderId=2,CustomerId=12,ListingId=2,CreateDate= new DateTime(2022,8,15)
        //        ,CheckInDate = new DateTime(2022,8,15)
        //        ,FinishDate = new DateTime(2022,8,17)
        //        ,StayNight=2,PaymentType=1,Adults=2,Children=0,Infants=0,UnitPrice=2990,Status=2,TranStatus=3},
        //        new Order{OrderId=3,CustomerId=13,ListingId=3,CreateDate= new DateTime(2022,8,18)
        //        ,CheckInDate = new DateTime(2022,8,18)
        //        ,FinishDate = new DateTime(2022,8,20)
        //        ,StayNight=2,PaymentType=1,Adults=2,Children=0,Infants=0,UnitPrice=2990,Status=2,TranStatus=2},
        //         new Order{OrderId=4,CustomerId=14,ListingId=1,CreateDate= new DateTime(2022,9,10)
        //        ,CheckInDate = new DateTime(2022,9,10)
        //        ,FinishDate = new DateTime(2022,9,12)
        //        ,StayNight=2,PaymentType=1,Adults=2,Children=0,Infants=0,UnitPrice=1990,Status=2,TranStatus=3},
        //        new Order{OrderId=5,CustomerId=15,ListingId=12,CreateDate= new DateTime(2022,9,15)
        //        ,CheckInDate = new DateTime(2022,9,15)
        //        ,FinishDate = new DateTime(2022,9,17)
        //        ,StayNight=2,PaymentType=1,Adults=2,Children=0,Infants=0,UnitPrice=2990,Status=2,TranStatus=2},
        //        new Order{OrderId=6,CustomerId=16,ListingId=9,CreateDate= new DateTime(2022,9,18)
        //        ,CheckInDate = new DateTime(2022,9,18)
        //        ,FinishDate = new DateTime(2022,9,20)
        //        ,StayNight=2,PaymentType=1,Adults=2,Children=0,Infants=0,UnitPrice=14000,Status=3,TranStatus=3},
        //        new Order{OrderId=7,CustomerId=16,ListingId=11,CreateDate= new DateTime(2021,11,30)
        //        ,CheckInDate = new DateTime(2021,12,23)
        //        ,FinishDate = new DateTime(2021,12,26)
        //        ,StayNight=3,PaymentType=1,Adults=2,Children=0,Infants=0,UnitPrice=4999,Status=2,TranStatus=2},
        //        new Order{OrderId=8,CustomerId=16,ListingId=6,CreateDate= new DateTime(2022,3,24)
        //        ,CheckInDate = new DateTime(2022,5,28)
        //        ,FinishDate = new DateTime(2022,6,1)
        //        ,StayNight=5,PaymentType=1,Adults=2,Children=0,Infants=0,UnitPrice=3500,Status=2,TranStatus=1},
        //        new Order{OrderId=9,CustomerId=16,ListingId=5,CreateDate= new DateTime(2022,1,3)
        //        ,CheckInDate = new DateTime(2022,2,4)
        //        ,FinishDate = new DateTime(2022,2,7)
        //        ,StayNight=3,PaymentType=1,Adults=3,Children=1,Infants=0,UnitPrice=1590,Status=3,TranStatus=1},
        //        new Order{OrderId=10,CustomerId=16,ListingId=2,CreateDate= new DateTime(2022,10,1)
        //        ,CheckInDate = new DateTime(2022,10,24)
        //        ,FinishDate = new DateTime(2022,10,27)
        //        ,StayNight=3,PaymentType=1,Adults=1,Children=0,Infants=0,UnitPrice=1350,Status=3,TranStatus=1},

        //    };
        //}
        public static List<Comment> Comments()
        {
            return new List<Comment>
            {
                new Comment{CommentId=1,OrderId=1,Rating=5,CreatTime=new DateTime(2022,9,10),HostId=1},
                new Comment{CommentId=2,OrderId=2,Rating=3,CreatTime=new DateTime(2022,9,10),HostId=1},
                new Comment{CommentId=3,OrderId=3,Rating=4,CreatTime=new DateTime(2022,9,10),HostId=2},
                new Comment{CommentId=4,OrderId=4,Rating=2,CreatTime=new DateTime(2022,9,10),HostId=1},
                new Comment{CommentId=5,OrderId=5,Rating=2,CreatTime=new DateTime(2022,9,10),HostId=1},
                new Comment{CommentId=6,OrderId=6,Rating=4,CreatTime=new DateTime(2022,9,10),HostId=2},
            };
        }
        public static List<Calendar> Calendars()
        {
            return new List<Calendar>
            {
                new Calendar{CalendarId = 1, CalendarDate = new DateTime(2023,1,30), Price = 1500, Available = false, ListingId = 1},
                new Calendar{CalendarId = 2, CalendarDate = new DateTime(2023,1,20), Price = 1500, Available = false, ListingId = 1},
                new Calendar{CalendarId = 3, CalendarDate = new DateTime(2023,1,10), Price = 2500, Available = true, ListingId = 1},
                new Calendar{CalendarId = 4, CalendarDate = new DateTime(2022,10,28), Price = 1300, Available = false, ListingId = 1},
                new Calendar{CalendarId = 5, CalendarDate = new DateTime(2022,10,29), Price = 1500, Available = false, ListingId = 1},
                new Calendar{CalendarId = 6, CalendarDate = new DateTime(2022,10,30), Price = 999, Available = true, ListingId = 1},
                new Calendar{CalendarId = 7, CalendarDate = new DateTime(2022,10,31), Price = 999, Available = true, ListingId = 1},
            };
        }
        public static List<Rating> Rating()
        {
            string Json = LoadJson(@"wwwroot/assert/comment.json");
            List<Rating> loadComment = JsonConvert.DeserializeObject<List<Rating>>(Json);
            return loadComment;
        }

    }
}
