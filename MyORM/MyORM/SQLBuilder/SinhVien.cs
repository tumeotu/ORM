using System;
using System.Collections.Generic;
using System.Text;

namespace MyORM.SQLBuilder
{
    class SinhVien
    {
        public int ID { get; set; }
        public string name { get; set; }
        public double DTB { get; set; }
        public SinhVien(string name, int ID, double dtb)
        {
            this.ID = ID;
            this.name = name;
            this.DTB = dtb;
        }
        public SinhVien()
        {
        }
    }
}
