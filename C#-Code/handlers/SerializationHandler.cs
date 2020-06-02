using System;
using System.Collections.Generic;
using System.Text;

namespace Demo.Hotel
{
    class SerializationHandler
    {
        string SerializationPath;

        public SerializationHandler()
        {
        }

        public SerializationHandler(string path,string folder, string name)
        { 
        //           SerializationPath = path & "\" & folder & "\" & name
        //If Not Directory.Exists(path & "\" & folder) Then
        //    Directory.CreateDirectory(path & "\" & folder)
        //End If
        }

        public SerializationHandler(string path, string name)
        { 
            //SerializationPath = path & "\" & name
        }

    //     Public Sub Serialize(Of T)(ByVal objectRoot As T)
    //    Dim fs As FileStream = File.Open(SerializationPath, FileMode.Create)
    //    Dim bf As New BinaryFormatter()
    //    bf.Serialize(fs, objectRoot)
    //    fs.Close()
    //End Sub
    //Public Function DeSerialize(Of T As Class)() As T
    //    Dim fs As FileStream = File.Open(SerializationPath, FileMode.Open)
    //    Dim bf As New BinaryFormatter()
    //    Dim dishes As T = TryCast(bf.Deserialize(fs), T)
    //    fs.Close()
    //    Return dishes
    //End Function


    }
}
