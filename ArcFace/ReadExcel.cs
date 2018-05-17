using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using System.Data.OleDb;
using Newtonsoft.Json;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using System.Drawing;

namespace ArcFace
{
    public class ReadExcel
    {
        public static DataTable ExcelToDS(string Path)
        {
            string strConn = "Provider=Microsoft.Jet.OLEDB.4.0;" + "Data Source=" + Path + ";" + "Extended Properties=Excel 8.0;";
            OleDbConnection conn = new OleDbConnection(strConn);
            conn.Open();
            string strExcel = "";
            OleDbDataAdapter myCommand = null;
            DataSet ds = null;
            strExcel = "select * from [sheet1$]";
            myCommand = new OleDbDataAdapter(strExcel, strConn);
            ds = new DataSet();
            myCommand.Fill(ds, "table1");
            return ds.Tables[0];
        }

        /// <summary>
        /// 解析JSON字符串生成对象实体
        /// </summary>
        /// <typeparam name="T">对象类型</typeparam>
        /// <param name="json">json字符串(eg.{"ID":"112","Name":"石子儿"})</param>
        /// <returns>对象实体</returns>
        public static T DeserializeJsonToObject<T>(string json) where T : class
        {
            JsonSerializer serializer = new JsonSerializer();
            StringReader sr = new StringReader(json);
            object o = serializer.Deserialize(new JsonTextReader(sr), typeof(T));
            T t = o as T;
            return t;
        }

        /// <summary>
        /// 解析JSON数组生成对象实体集合
        /// </summary>
        /// <typeparam name="T">对象类型</typeparam>
        /// <param name="json">json数组字符串(eg.[{"ID":"112","Name":"石子儿"}])</param>
        /// <returns>对象实体集合</returns>
        public static List<T> DeserializeJsonToList<T>(string json) where T : class
        {
            JsonSerializer serializer = new JsonSerializer();
            StringReader sr = new StringReader(json);
            object o = serializer.Deserialize(new JsonTextReader(sr), typeof(List<T>));
            List<T> list = o as List<T>;
            return list;
        }

        public static string ReadJson()
        {
            StreamReader sr = new StreamReader(@"D:\FeatureData\readydata\luckinfo.json", Encoding.Default);
            String line;
            string jsonobj = "";
            while ((line = sr.ReadLine()) != null)
            {
                jsonobj = jsonobj + line.ToString();
            }
            return jsonobj;
        }

        public static List<Staff> ReadyData()
        {
            var json = ReadJson();
            var result = DeserializeJsonToList<Staff>(json);
            return result;
        }

        public static void RegisterImg()
        {
            var staffs = ReadyData();
            var _RegisterIndex = 0;
            byte[] _RegisterFeatureData = null;
            try
            {
                for (var i = 0; i < staffs.Count; i++)
                {
                    if (i == 3)
                    {

                    }
                    Image img = new Bitmap("D:\\FeatureData\\readydata\\UATImages\\" + staffs[i].StaffID + ".jpeg");
                    _RegisterFeatureData = Api.CacheFaceResults[_RegisterIndex].GetFeatureData();
                    Api.AddFace(staffs[i].StaffID, _RegisterFeatureData, img, staffs[i].Name, staffs[i].SeatNumber);
                    _RegisterIndex += 1;
                   
                }
            }
            catch (Exception)
            {
                
            }

        }

    }
}
