using Newtonsoft.Json.Linq;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ServerHeartBeat.Base
{
    public abstract class BaseSetting<T>
    {

        public abstract string Path { get; }

        public delegate void Updated();

        protected List<T> data;

        public virtual List<T> DataSource
        {
            get
            {
                if (data == null)
                {
                    FileInfo dataFile = new FileInfo(Path);
                    if (!dataFile.Exists)
                    {
                        data = new List<T>();
                        Update();
                    }
                    else
                    {
                        data = JsonConvert.DeserializeObject<List<T>>(File.ReadAllText(Path));
                    }
                }

                return data;
            }
        }
        

        /// <summary>
        /// 接口更新后
        /// </summary>
        public event Updated UpdatedEvent;

        public virtual void Update()
        {
            File.WriteAllText(Path, JsonConvert.SerializeObject(DataSource));
            if (UpdatedEvent != null)
            {
                UpdatedEvent();
            }
        }
    }

    public class Data<T> : BaseSetting<T>
    {
        public override string Path { get { return "data.json"; } }

    }

    public class DataType<T> : BaseSetting<T>
    {
        public override string Path { get { return "type.json"; } }
    }

    public static class Setting
    {
        private static BaseSetting<Models.Api> data;
        public static BaseSetting<Models.Api> Data
        {
            get
            {
                if (data == null)
                {
                    data = new Data<Models.Api>();
                }
                return data;
            }
        }

        private static BaseSetting<Models.ApiType> type;
        public static BaseSetting<Models.ApiType> Type
        {
            get
            {
                if (type == null)
                {
                    type = new DataType<Models.ApiType>();
                }
                return type;
            }
        }
    }
}
