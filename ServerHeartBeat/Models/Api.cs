using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel;
using Newtonsoft.Json;
using System.Net;
using Newtonsoft.Json.Linq;
using System.IO;

namespace ServerHeartBeat.Models
{
    public class Api
    {
        public int ID { get; set; }

        /// <summary>
        /// 名称
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// 连接
        /// </summary>
        public string Link { get; set; }

        /// <summary>
        /// 连接头
        /// </summary>
        public int Type { get; set; }

        /// <summary>
        /// 全连接
        /// </summary>
        public string FullLink
        {
            get
            {
                return Base.Setting.Type.DataSource.FirstOrDefault(s => s.ID == Type).Link + Link;
            }
        }

        /// <summary>
        /// 是否启用
        /// </summary>
        public bool Enable { get; set; }

        /// <summary>
        /// 间隔
        /// </summary>
        public int Interval { get; set; }

        /// <summary>
        /// 参数
        /// </summary>
        public string Parameter { get; set; }

        public Dictionary<string, object> GetDicParameter()
        {

            if (!string.IsNullOrWhiteSpace(Parameter))
            {
                return JsonConvert.DeserializeObject<Dictionary<string, object>>(Parameter);
            }
            else
            {
                return new Dictionary<string, object>();
            }

        }

        /// <summary>
        /// 请求类型
        /// </summary>
        public string RequestType { get; set; }

        /// <summary>
        /// 最后执行时间
        /// </summary>
        public DateTime LastRunDataTime { get; set; } = DateTime.Now;


        public delegate void CreateRequest(string state, string message);

        /// <summary>
        /// 请求成功后
        /// </summary>
        public event CreateRequest SuccessEvent;

        /// <summary>
        /// 请求失败后
        /// </summary>
        public event CreateRequest ErrorEvent;

        /// <summary>
        /// 创建请求返回Jobject
        /// </summary>
        /// <param name="url">请求地址</param>
        /// <param name="type">类别</param>
        /// <param name="p">参数</param>
        /// <returns></returns>
        public async void Request()
        {
            if (!Enable || LastRunDataTime.AddSeconds(Interval) > DateTime.Now)
            {
                return;
            }
            string link = FullLink;
            LastRunDataTime = DateTime.Now;
            var dir = GetDicParameter();
            if (RequestType == "GET" && dir.Count > 0)
            {
                var arrDir = dir.Select(s => $"{s.Key}={s.Value}").ToArray();
                link = $"{link}?{string.Join("&", arrDir)}";
            }
            System.Net.HttpWebRequest request = (System.Net.HttpWebRequest)System.Net.WebRequest.Create(link);
            request.Method = RequestType;
            request.ContentType = "application/x-www-form-urlencoded";
            if (RequestType == "POST" && dir.Count == 0)
            {
                request.ContentLength = 0;
            }

            System.Text.StringBuilder sbPostData = new System.Text.StringBuilder();
            int i = 0;
            if (Parameter != null && dir.Keys.Count > 0)
            {
                foreach (var key in dir.Keys)
                {
                    if (i == 0)
                    {
                        sbPostData.AppendFormat("{0}={1}", key, dir[key]);
                    }
                    else
                    {
                        sbPostData.AppendFormat("&{0}={1}", key, dir[key]);
                    }
                    i++;
                }

                //var data = System.Text.Encoding.UTF8.GetBytes(sbPostData.ToString());
                //using (var stream = request.GetRequestStream())
                //{
                //    stream.Write(data, 0, data.Length);
                //}
            }
            try
            {
                var response = await request.GetResponseAsync();
                var steam = response.GetResponseStream();
                string txtData = "";
                using (var reader = new StreamReader(steam))
                {
                    txtData = reader.ReadToEnd();
                }
                var result = JsonConvert.DeserializeObject<JObject>(txtData);
                var state = result["CyState"].Value<string>();
                var message = result["CyMessage"].Value<string>();

                if (state == "Success")
                {
                    SuccessEvent(state, message);
                }
                else
                {
                    ErrorEvent(state, message);
                }
            }
            catch (Exception ex)
            {
                ErrorEvent(ex.GetType().Name, ex.Message);
            }

        }
    }

    public class ApiViewModel
    {
        public ApiViewModel(Api api)
        {
            ID = api.ID;
            Name = api.Name;
            Link = api.FullLink;
            Enable = api.Enable;
            Interval = api.Interval;
        }

        public int ID { get; set; }

        [DisplayName("名字")]
        public string Name { get; set; }

        [DisplayName("名字")]
        public string Link { get; set; }

        [DisplayName("启用")]
        public bool Enable { get; set; }

        [DisplayName("间隔（秒）")]
        public int Interval { get; set; }


    }

    public class ApiType
    {
        public int ID { get; set; }

        public string Link { get; set; }
    }
}
