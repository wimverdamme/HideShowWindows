using System;
using System.IO;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Formatters.Binary;

namespace HideShowWindows
{
    [Serializable()]
    public class HiddenDialog: ISerializable
    {
        private string title;
        private int hWnd;

        public string getTitle()
        {
            return title;
        }
        public int gethWnd()
        {
            return hWnd;
        }

        public HiddenDialog(string title, int hWnd)
        {
            this.title = title;
            this.hWnd = hWnd;
        }

        public HiddenDialog(SerializationInfo info, StreamingContext ctxt)
        {
            this.title = (string)info.GetValue("title", typeof(string));
            this.hWnd = (int)info.GetValue("hWnd", typeof(int));            
        }

        public void GetObjectData(SerializationInfo info, StreamingContext ctxt)
        {
            info.AddValue("title", this.title);
            info.AddValue("hWnd", this.hWnd);
        }
    }

    [Serializable()]
    public class ObjectToSerialize : ISerializable
    {
        private List<HiddenDialog> hiddenDialogs;

        public List<HiddenDialog> HiddenDialogs
        {
            get { return this.hiddenDialogs; }
            set { this.hiddenDialogs = value; }
        }

        public ObjectToSerialize()
        {
        }


        public ObjectToSerialize(SerializationInfo info, StreamingContext ctxt)
        {
            this.hiddenDialogs = (List<HiddenDialog>)info.GetValue("HiddenDialogs", typeof(List<HiddenDialog>));
        }

        public void GetObjectData(SerializationInfo info, StreamingContext ctxt)
        {
            info.AddValue("HiddenDialogs", this.hiddenDialogs);
        }
    }

    public class Serializer
    {
        public Serializer()
        {
        }

        public void SerializeObject(string filename, ObjectToSerialize objectToSerialize)
        {
            Stream stream = File.Open(filename, FileMode.Create);
            BinaryFormatter bFormatter = new BinaryFormatter();
            bFormatter.Serialize(stream, objectToSerialize);
            stream.Close();
        }

        public ObjectToSerialize DeSerializeObject(string filename)
        {
            ObjectToSerialize objectToSerialize;
            Stream stream = File.Open(filename, FileMode.Open);
            BinaryFormatter bFormatter = new BinaryFormatter();
            objectToSerialize = (ObjectToSerialize)bFormatter.Deserialize(stream);
            stream.Close();
            return objectToSerialize;
        }
    }
}
