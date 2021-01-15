using MyORM.Mapper;
using Npgsql;
﻿using MyORM.Extension;
using MyORM.SQLBuilder;
using System;
using System.Collections.Generic;
using System.Data;

namespace abc
{
    public class Class1
    {
        static void Main(string[] args)
        {

            SinhVien sinhVien = new SinhVien();
            sinhVien.name = "sdsd";
            sinhVien.DTB = 305;
            SqlString<SinhVien> sqlString = new SqlString<SinhVien>();

            //sqlString.Update(sinhVien).Where(sv => sv.name == "sff");
            //Console.WriteLine(sqlString.sql);

            //sqlString.save(sinhVien);
            //Console.WriteLine(sqlString.sql);

            //sqlString.SelectAll().Where(sv => sv.name == "ss dfd").AND(sv => sv.DTB >= 5);
            //Console.WriteLine(sqlString.sql);
            var table = new DataTable();
            string query = "SELECT sinhvien.id as \"sinhvien.id\", sinhvien.name as \"sinhvien.name\", sinhvien.gpa as \"sinhvien.gpa\"  FROM sinhvien";
            string connectionString = "Server=127.0.0.1;Port=5432;Database=gomoku-online;User Id=postgres;Password=1;";
            using (var da = new NpgsqlDataAdapter(query, connectionString))
            {
                da.Fill(table);
                foreach (DataColumn col in table.Columns)
                {
                    Console.WriteLine(col.ColumnName);
                }
                foreach (DataRow row in table.Rows)
                {
                    Console.WriteLine(row["sinhvien.id"].ToString() + " - " + row["sinhvien.name"].ToString() + " - " + row["sinhvien.gpa"].ToString());
                }
                DataMapper dataMapper = new DataMapper();
                List<SinhVien> sv = dataMapper.loadAll<SinhVien>(table);
                foreach (var sinhvien in sv)
                {
                    Console.WriteLine(sinhvien.ID + " - " + sinhvien.name + " - " + sinhvien.DTB);
                }
            }

        }
    }
}
