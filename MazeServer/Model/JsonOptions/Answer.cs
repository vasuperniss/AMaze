﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Script.Serialization;

namespace MazeServer.Model.JsonOptions
{
    class Answer
    {
        private int type;
        private string data;

        public int Type
        {
            get { return type; }
            set { type = value;  }
        }

        public string Content
        {
            get { return data; }
            set { data = value; }
        }

        public string GetJSONAnswer(int type, IServerAnswer ans)
        {
            JavaScriptSerializer serializer = new JavaScriptSerializer();
            this.type = type;
            this.data = serializer.Serialize(ans);

            return serializer.Serialize(this);
        }
    }
}