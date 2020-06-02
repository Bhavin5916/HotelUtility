using System;
using System.Collections.Generic;
using System.Text;

namespace Demo.Hotel
{
    class Xml_Handler
    {
        public void Create_xml_firsttime(string p1)
        {
            //    Dim table_xml As String
            //table_xml = p1
            //Dim wrt_set As XmlWriterSettings = New XmlWriterSettings()
            //wrt_set.Indent = True
            //Using writer As XmlWriter = XmlWriter.Create("table'" & p1 & "'.xml")
            //    writer.WriteStartDocument()
            //    writer.WriteStartElement("table")
            //    writer.WriteEndElement()
            //    writer.WriteEndDocument()
            //End Using
        }

        public void create_tableXml(string p1)
        {
            //    Dim table_xml As String
            //table_xml = p1
            //Dim wrt_set As XmlWriterSettings = New XmlWriterSettings()
            //wrt_set.Indent = True
            //Using writer As XmlWriter = XmlWriter.Create("table'" & p1 & "'.xml")
            //    If Table_billing.DataGridView1.Rows.Count = 0 Then
            //        writer.WriteStartDocument()
            //        writer.WriteStartElement("table")
            //        writer.WriteEndElement()
            //        writer.WriteEndDocument()
            //    Else
            //        writer.WriteStartDocument()
            //        writer.WriteStartElement("table")
            //        For Each Dgvr As DataGridViewRow In Table_billing.DataGridView1.Rows
            //            writer.WriteStartElement("ItemDetail")
            //            writer.WriteElementString("Table_No", Convert.ToInt32(p1))
            //            writer.WriteElementString("Item_Catagory", Dgvr.Cells(0).Value)
            //            writer.WriteElementString("Item_name", Dgvr.Cells(1).Value)
            //            writer.WriteElementString("Item_prize", Dgvr.Cells(2).Value)
            //            writer.WriteElementString("Item_quntity", Dgvr.Cells(3).Value)
            //            writer.WriteElementString("Item_total", Dgvr.Cells(4).Value)
            //            writer.WriteEndElement()
            //        Next
            //        writer.WriteEndElement()
            //        writer.WriteEndDocument()
            //    End If
            //End Using
        }
        public void addnew_row()
        {
            //     Dim table_xml As String
            //Dim total_bill As Double = 0
            //table_xml = p1
            //My.Computer.FileSystem.DeleteFile("table'" & p1 & "'.xml")
            //Dim wrt_set As XmlWriterSettings = New XmlWriterSettings()
            //wrt_set.Indent = True
            //Using writer As XmlWriter = XmlWriter.Create("table'" & p1 & "'.xml")
            //    writer.WriteStartDocument()
            //    writer.WriteStartElement("table")
            //    For Each Dgvr As DataGridViewRow In Table_billing.DataGridView1.Rows
            //        writer.WriteStartElement("ItemDetail")
            //        writer.WriteElementString("Table_No", Convert.ToInt32(p1))
            //        writer.WriteElementString("Item_catagory", Dgvr.Cells(0).Value)
            //        writer.WriteElementString("Item_name", Dgvr.Cells(1).Value)
            //        writer.WriteElementString("Item_prize", Dgvr.Cells(2).Value)
            //        writer.WriteElementString("Item_quntity", Dgvr.Cells(3).Value)
            //        writer.WriteElementString("Item_total", Dgvr.Cells(4).Value)
            //        total_bill = total_bill + Convert.ToDouble(Dgvr.Cells(4).Value)
            //        writer.WriteElementString("Total_bill", total_bill)
            //        writer.WriteEndElement()
            //    Next
            //    writer.WriteEndElement()
            //    writer.WriteEndDocument()
            //End Using
            //bind_gridview_from_xml(p1)
        }
        public void bind_gridview_from_xml(string p1)
        { 
        //     Table_billing.DataGridView1.Rows.Clear()
        //Dim xmldoc As New XmlDocument
        //Dim xml_node As XmlNodeList
        //Dim i As Integer
        //Dim fs As New FileStream("table'" & p1 & "'.xml", FileMode.Open, FileAccess.Read)
        //xmldoc.Load(fs)
        //xml_node = xmldoc.GetElementsByTagName("ItemDetail")
        //If xml_node.Count = 0 Then
        //    Table_billing.lbl_total_final.Text = 0
        //Else
        //    For i = 0 To xml_node.Count - 1 Step 1
        //        Table_billing.DataGridView1.Rows.Add(
        //                                            xml_node(i).ChildNodes.Item(1).InnerText.Trim(),
        //                                            xml_node(i).ChildNodes.Item(2).InnerText.Trim(),
        //                                              xml_node(i).ChildNodes.Item(3).InnerText.Trim(),
        //                                          xml_node(i).ChildNodes.Item(4).InnerText.Trim(),
        //                                      xml_node(i).ChildNodes.Item(5).InnerText.Trim())
        //        Table_billing.lbl_total_final.Text = xml_node(i).ChildNodes.Item(6).InnerText.Trim()
        //    Next
        //End If
        //fs.Close()
        //fs.Dispose()
        }
    }
}
