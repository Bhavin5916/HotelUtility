using System;
using System.Collections.Generic;
using System.Text;

namespace Demo.Hotel.handlers
{
    class Pertablebill
    {
        bool issaved = false;
        decimal totalwithouttax;
        decimal tax = Convert.ToDecimal(0);
        decimal tatalwithtax = Convert.ToDecimal(0);
        string discount = "";


        public int _tableNo { get; set; }

        public List<Aclass> _bill = new List<Aclass>();
        //    public property List<Aclass>  Bill() As List(Of Aclass)
        //    Get
        //        Return _bill
        //    End Get
        //    Set(ByVal value As List(Of Aclass))
        //        _bill = value
        //    End Set
        //End Property
    }
}
