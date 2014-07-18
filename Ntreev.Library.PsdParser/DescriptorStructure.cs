using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace Ntreev.Library.PsdParser
{
    internal class DescriptorStructure : Properties
    {
        public string classId;
        //public int descCount;
        //public Description[] descs;
        public string name;

        public DescriptorStructure(BinaryReader br)
        {
            this.name = PSDTypeToolObject2.Decoder.readUnicodeString(br);
            this.classId = PSDTypeToolObject2.Decoder.readStringOrKey(br);
            int count = EndianReverser.getInt32(br);
            for (int i = 0; i < count; i++)
            {
                Description desc = new Description();
                desc.load(br);

                this.Add(desc.key, desc.value);
            }
        }

        //public bool ContainsProperty(string property)
        //{
        //    string[] ss = property.Split(new char[] { '.', '[', ']', }, StringSplitOptions.RemoveEmptyEntries);

        //    object value = this;

        //    foreach (var item in ss)
        //    {
        //        if (value is ArrayList == true)
        //        {
        //            ArrayList arrayList = value as ArrayList;
        //            int index;
        //            if (int.TryParse(item, out index) == false)
        //                return false;
        //            if (index >= arrayList.Count)
        //                return false;
        //            value = arrayList[index];
        //        }
        //        else if (value is Dictionary<string, object> == true)
        //        {
        //            Dictionary<string, object> props = value as Dictionary<string, object>;
        //            if (props.ContainsKey(item) == false)
        //            {
        //                return false;
        //            }

        //            value = props[item];
        //        }

        //    }
        //    return true;
        //}

        //public object GetProperty(string property)
        //{
        //    string[] ss = property.Split(new char[] { '.', '[', ']', }, StringSplitOptions.RemoveEmptyEntries);

        //    object value = this;

        //    foreach (var item in ss)
        //    {
        //        if (value is ArrayList == true)
        //        {
        //            ArrayList arrayList = value as ArrayList;
        //            value = arrayList[int.Parse(item)];
        //        }
        //        else if (value is Dictionary<string, object> == true)
        //        {
        //            Dictionary<string, object> props = value as Dictionary<string, object>;
        //            value = props[item];
        //        }

        //    }
        //    return value;
        //}

        internal class Description
        {
            public object value;
            public string key;
            public string ostype;

            public void load(BinaryReader br)
            {
                this.key = PSDTypeToolObject2.Decoder.readStringOrKey(br).Trim();
                this.ostype = PSDUtil.readAscii(br, 4);
                PSDTypeToolObject2.Decoder.DecodeFunc func = PSDTypeToolObject2.Decoder.getDecoder(this.ostype);
                if (func != null)
                {
                    this.value = func(br, this.key);
                }
            }
        }
    }
}
