﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Data;
using System.Drawing;
using System.Diagnostics;
using System.ComponentModel;
using System.Text.RegularExpressions;
using System.Collections;
using MySql.Data.MySqlClient;


namespace adc
{
    /// <summary>
    /// searchDocumentPage01.xaml에 대한 상호 작용 논리
    /// </summary>
    public partial class searchDocumentPage01 : Page
    {
        List<string> tags;
        static string db_information = @"SERVER=127.0.0.1;DATABASE=adcs;UID=godocx;PASSWORD=486; ";

        public searchDocumentPage01(List<string> vs) // 태그 리스트로 받아옴 
        {
            InitializeComponent();
            tags = vs;
            /*
             var connectionString = "SERVER=localhost;DATABASE=adcs;UID=root;PASSWORD=ewhayeeun;";
             var connection = new MySqlConnection(connectionString);
            */
            MySqlConnection connection = new MySqlConnection(db_information);
            try
            {
                connection.Open();
                MySqlCommand cmd = new MySqlCommand("SELECT DISTINCT CONTENT_TAG FROM CONTENT", connection);
                MySqlDataAdapter adp = new MySqlDataAdapter(cmd);
                DataSet ds = new DataSet();

                adp.Fill(ds, "loadDataBinding"); //content table
               
            }
            catch (MySqlException ex)
            {
                MessageBox.Show(ex.ToString());
            }
            finally
            {
                connection.Close();
            }

        }


        private void BtntoMain(object sender, RoutedEventArgs e)
        {
            Home page = new Home();
            NavigationService.Navigate(page);
        }

        // data Table --> replace to DB later
        private void Page_Loaded(object sender, RoutedEventArgs e)
        {
            
            List<int> ID_list = new List<int>();
           
            int value =0;
            int tagRowCount=0;

            string sql1, sql2, sql3;
           
            DataTable dataTable = new DataTable();
            dataTable.Columns.Add("ID", typeof(string));
            dataTable.Columns.Add("NAME", typeof(string));
            dataTable.Columns.Add("TYPE_TAG", typeof(string));
            dataTable.Columns.Add("CONTENT_TAG", typeof(string));
            dataTable.Columns.Add("PATH", typeof(string));


            string ttag = "";           // 형식태그 String'형식1'
            string ctags = @"";         // 내용태그 정규표현식 형태로 (@".*태그1.*태그3.*")
            string ctag = "";
            var tag_list = new List<string>();
            int length = tags.Count();
            if (ttag != "") ttag = "";
            if (ctags != "") ctags = "";
            if (ctag != "") ctag = "";
            foreach (string i in tags)
            {
                //ctags = ctags + i 
                if (tags.IndexOf(i) == 0)
                {
                    ttag = ttag + i;
                  //  tag_list.Add(i);
                }
                else
                {
                    ctags = ctags + ".*" + i;
                    ctag = ctag + i;
                    tag_list.Add(i);
                };

            }
            ctags = ctags + ".*";
            Console.WriteLine(" ");
            Console.WriteLine(ttag);
            Console.WriteLine(ctags);
            Console.WriteLine(ctag);
            foreach(string str in tag_list)
            {
                Console.WriteLine(str);
            }

            try
            {
                using (MySqlConnection conn = new MySqlConnection(db_information))
                {
                    

                    // command에 query 삽입
                    
                    sql1 = "SELECT * FROM document WHERE TYPE_TAG =@val1;"; // SQL 형식태그 선택
                    MySqlCommand cmd1 = new MySqlCommand();
                    cmd1.CommandText = sql1;
                    cmd1.Parameters.AddWithValue("@val1", ttag); 
                    
                    
                    // 모두 읽어온 후 나중에 내용태그 값
                    sql2 = "SELECT * FROM content;";  //  내용태그 
                    MySqlCommand cmd2 = new MySqlCommand();
                    cmd2.CommandText = sql2;


                    sql3 = "SELECT * FROM content;";  //  내용태그 
                    MySqlCommand cmd3 = new MySqlCommand();
                    cmd3.CommandText = sql3;
                   

                    DataSet ds = new DataSet();
                    cmd1.Connection = conn;     // 형식 DB 연결
                    cmd2.Connection = conn;    // 내용 DB 연결 (ID 찾기)
                    cmd3.Connection = conn;     //내용 DB 연결 (전체)

                    conn.Open();
                    if (conn.State == System.Data.ConnectionState.Open)
                    {
                        MessageBox.Show("서버에 연결");
                        Console.WriteLine("서버에 연결");

                    }

                    // 형식 DB 테이블 읽어오기
                    MySqlDataAdapter adapter = new MySqlDataAdapter(cmd1);
                    adapter.Fill(ds, "First Table"); // type table 

                    // 내용 DB에서 내용태그에 해당하는 ID 찾기 
                    adapter = new MySqlDataAdapter(cmd2);
                    adapter.Fill(ds, "Second Table"); // content table

                    // 내용 DB 읽어오기 
                    adapter = new MySqlDataAdapter(cmd3);
                    adapter.Fill(ds, "Third Table"); // content table

                    // 형식태그 입력값과 일치하는 문서 ID를 배열로 저장하기
                    //dataTable = ds.Tables["First Table"];
                  
                    tagRowCount = ds.Tables[0].Rows.Count;
                    ID_list.Capacity = tagRowCount;
                    if (ds.Tables.Count > 0)
                    {
                        foreach (DataRow dr in ds.Tables[0].Rows)
                        {

                            //  ID_list[Convert.ToInt32(dr["ID"])] = i;
                            ID_list.Add(Convert.ToInt32(dr["ID"]));
                            Console.Write(dr["NAME"]);
                            Console.Write("  ");
                            Console.WriteLine(dr["TYPE_TAG"]);
                           
                        }
                        
                    }
                   
                   
                    Console.WriteLine("내용태그");

                    // 형식태그가 일치하는 문서들의 ID 리스트를 doc[0]에 입력
         
                    //num 번 ID 값을 갖는 문서에 대해 태그 몇개 갖는지 count
                    var myTable = new Dictionary<int, int>();
                    foreach (DataRow dr in ds.Tables[1].Rows)
                    {
                        int i = Convert.ToInt32(dr["ID"]);

                        if (!ID_list.Contains(i)) continue;
                        else
                        {
                            foreach (String s in tag_list)
                            {
                                String content_tag = dr["Content_Tag"].ToString();
                                if (content_tag == s)
                                {
                                    if (!myTable.ContainsKey(i))
                                        myTable.Add(i, 1);
                                    else
                                    {
                                        value = myTable[i];
                                        //Console.WriteLine(value++);
                                        myTable[i] = ++value;
                                    }


                                }
                                else if (content_tag != s) continue;

                            }
                        }
                    }
                    // myTable 내림차순 정렬 
                    var queryDesc = myTable.OrderByDescending(x => x.Value);
                    /*
                    foreach (KeyValuePair<int, int> kvp in myTable)
                    {
                        Console.WriteLine("key={0}, Value={1}", kvp.Key, kvp.Value);
                    }
                    */
                   foreach (var dictionary in queryDesc)
                    {
                        Console.WriteLine("key={0}, Value={1}", dictionary.Key, dictionary.Value);
                    }
                   


                    DataRow row = null;
                    List<string> ctagData = new List<string>();
                    // 내용태그와 형식태그 결과 하나의 테이블 합쳐서 화면에 바인딩 
                   
                    //foreach (int i in ID_list)  //list : ID 리스트 
                    foreach(var dictionary in queryDesc)
                    {
                        Console.WriteLine(dictionary.Key);
                        foreach (DataRow dr in ds.Tables[0].Rows)
                        {
                            if (Convert.ToInt32(dr["ID"]) == dictionary.Key)
                            {
                                // dataTable.Rows.Add(new string[] { "1", "1.pdf", "논문", " 여성 봉사 복지", "C:\\capston" });
                                row = dataTable.NewRow();
                                row["ID"] = dictionary.Key;
                                row["NAME"] = dr["NAME"].ToString();
                                row["TYPE_TAG"] = dr["TYPE_TAG"].ToString();
                                //row["CONTENT_TAG"] = dr["CONTENT_TAG"];
                                row["PATH"] = dr["PATH"].ToString();
                                dataTable.Rows.Add(row);
                                break;
                            }
                        }
                    }

                    
                    int rowNum = 0;
                    //foreach (int i in ID_list)
                    foreach (var dictionary in queryDesc)
                    {

                        String Contents = "";
                        foreach (DataRow drc in ds.Tables[2].Rows)
                        {
                            if (Convert.ToInt32(drc["ID"]) == dictionary.Key)
                            {
                                Contents = Contents + drc["CONTENT_TAG"].ToString() + " ";
                                //Console.WriteLine(Contents);
                            }
                        }
                        dataTable.Rows[rowNum]["CONTENT_TAG"] = Contents;
                        rowNum++;
                    }




                    foreach (DataRow row_ in dataTable.Rows)
                    {
                        foreach (DataColumn column in dataTable.Columns)
                        {
                            Console.WriteLine(row_[column]);
                        }
                    }





                    dataGrid1.ItemsSource = dataTable.DefaultView;

                    sql3 = "SELECT * FROM document WHERE TYPE_TAG =@val1 " +
                           "UNION  ALL " +
                           "SELECT * FROM content ORDER BY ID;";

                    conn.Close();

                }


            }
            catch (Exception ex) { MessageBox.Show(ex.Message); }
            /*
            var  connectionString = "SERVER=localhost;DATABASE=adcs;UID=root;PASSWORD=ewhayeeun;";
            var connection = new MySqlConnection(connectionString);
            try
            {
                connection.Open();
                MySqlCommand cmd = new MySqlCommand("SELECT DISTINCT CONTENT_TAG FROM CONTENT", connection);
                MySqlDataAdapter adp = new MySqlDataAdapter(cmd);
                DataSet ds = new DataSet();

                adp.Fill(ds, "loadDataBinding"); //content table
                //DataSet ds = new DataSet();
                // 윈도우 폼의 LoadDataBinding에 데이터 넣기
                //adp.Fill(ds, "Page_Loaded");

                //dataGrid1.DataContext = ds;

            }
            catch (MySqlException ex)
            {
                MessageBox.Show(ex.ToString());
            }
            finally
            {
                connection.Close();
            }
            */

            //DataTable 생성
            //DataTable dataTable = new DataTable();
            // DataTable dataTable = ds.Tables[0];
           // var dtkey = new DataColumn[1];

            /*컬럼 생성
            dataTable.Columns.Add("ID", typeof(string));
            dataTable.Columns.Add("DATAPATH", typeof(string));
            dataTable.Columns.Add("TTAGLIST", typeof(string));
            dataTable.Columns.Add("CTAGLIST", typeof(string));
            dataTable.Columns.Add("FOLDERPATH", typeof(string));
            */

            // 이전 페이지에서 선택한 태그들
            // tags에 리스트로 담겨있고 ctags에 문자열로 모았음(list, string 둘중 편한 것 선택




            //test data
            /*
            dataTable.Rows.Add(new string[] { "1", "1.pdf", "논문", " 여성 봉사 복지", "C:\\capston" });
            dataTable.Rows.Add(new string[] { "2", "2.pdf", "논문", " 여성 복지", "C:\\capston" });
            dataTable.Rows.Add(new string[] { "3", "3.pdf", "논문", " 여성 ", "C:\\capston" });
            dataTable.Rows.Add(new string[] { "4", "4.pdf", "논문", " 봉사", "C:\\capston" });
            dataTable.Rows.Add(new string[] { "5", "5.pdf", "논문", " 복지 ", "C:\\capston" });

            dataTable.Rows.Add(new string[] { "6", "1.pdf", "기사", " 여성 봉사 복지", "C:\\capston" });
            dataTable.Rows.Add(new string[] { "7", "2.pdf", "기사", " 여성 복지", "C:\\capston" });
            dataTable.Rows.Add(new string[] { "8", "3.pdf", "기사", " 여성 ", "C:\\capston" });
            dataTable.Rows.Add(new string[] { "9", "4.pdf", "기사", " 봉사", "C:\\capston" });
            dataTable.Rows.Add(new string[] { "10", "5.pdf", "기사", " 복지 ", "C:\\capston" });

            dataTable.Rows.Add(new string[] { "11", "1.pdf", "지원서", " 여성 봉사 복지", "C:\\capston" });
            dataTable.Rows.Add(new string[] { "12", "2.pdf", "지원서", " 여성 복지", "C:\\capston" });
            dataTable.Rows.Add(new string[] { "13", "3.pdf", "지원서", " 여성 ", "C:\\capston" });
            dataTable.Rows.Add(new string[] { "14", "4.pdf", "지원서", " 봉사", "C:\\capston" });
            dataTable.Rows.Add(new string[] { "15", "5.pdf", "지원서", " 복지 ", "C:\\capston" });

            dataTable.Rows.Add(new string[] { "16", "1.pdf", "공고", " 여성 봉사 복지", "C:\\capston" });
            dataTable.Rows.Add(new string[] { "17", "2.pdf", "공고", " 여성 복지", "C:\\capston" });
            dataTable.Rows.Add(new string[] { "18", "3.pdf", "공고", " 여성 ", "C:\\capston" });
            dataTable.Rows.Add(new string[] { "19", "4.pdf", "공고", " 봉사", "C:\\capston" });
            dataTable.Rows.Add(new string[] { "20", "5.pdf", "공고", " 복지 ", "C:\\capston" });
            */

            //DataTable의 Default View를 바인딩하기 (원본 데이터테이블)

            // datacolumn으로 primary key 설정
            DataColumn[] primarykey = new DataColumn[1];
            //primarykey[0] = dataTable.Columns["ID"];

            // 복합키 설정 
            //dataTable.PrimaryKey = primarykey;
            /*
            // 형식태그 검색 결과 테이블 
            DataTable semiTable = new DataTable();
            semiTable.Columns.Add("ID", typeof(string));
            semiTable.Columns.Add("NAME", typeof(string));
            semiTable.Columns.Add("TYPE_TAG", typeof(string));
            semiTable.Columns.Add("PATH", typeof(string));

            // 내용태그 검색 결과 테이블 
            DataTable resultTable = new DataTable();
            resultTable.Columns.Add("ID", typeof(string));
            resultTable.Columns.Add("CONTENT_TAG", typeof(string));
            */
            /*
             * " 되살리기!!!!"

            // 1. 형식태그 선별 
            Console.WriteLine("형식태그");
            //string typeTag = "TTAGLIST = '논문'";
            string typeTag = string.Format("TYPE_TAG = '{0}'", ttag);
            Console.Write(typeTag);
            DataRow[] semiRows = dataTable.Select(typeTag);

            //신규 데이터 테이블에 row 추가
            foreach (DataRow dr in semiRows)
            {
                semiTable.Rows.Add(dr.ItemArray);
            }

            Console.WriteLine(semiTable.Rows.Count);
            for (int i = 0; i < semiTable.Rows.Count; i++)
            {
                for (int col = 0; col < semiTable.Columns.Count; col++)
                {
                    Console.Write("{0}  ", semiTable.Rows[i][col].ToString());
                }
                Console.WriteLine(" ");
            }


            // 2. 내용태그 선별 
            Console.WriteLine("내용태그");
            //Regex reg = new Regex(@".*태그1.*태그3.*"); // 태그1*태그3
            Regex reg = new Regex(ctags);
            //Regex reg = new Regex(@".*여성.*복지.*봉사.*");
            //matching rows
            ArrayList al = new ArrayList();
            foreach (DataRow row in semiTable.Select())
                if (reg.Match(row["CONTENT_TAG"].ToString()).Success)
                    al.Add(row);
            DataRow[] finalRows = (DataRow[])al.ToArray(typeof(DataRow));

            //display rows 
            foreach (var row in finalRows)
            {
                // Console.WriteLine(row["CTAGLIST"]);
                resultTable.Rows.Add(row.ItemArray);
            }
            Console.WriteLine(resultTable.Rows.Count);
            for (int i = 0; i < resultTable.Rows.Count; i++)
            {
                for (int col = 0; col < resultTable.Columns.Count; col++)
                {
                    Console.Write("{0} ", resultTable.Rows[i][col].ToString());
                }
                Console.WriteLine(" ");
            }
           
            //resultTable의 Default View를 바인딩하기 (원본 데이터테이블)
            dataGrid1.ItemsSource = resultTable.DefaultView; //re

            */

        }

        //FolderBrowserDialog + System.Windows.Forms
        private void openFolder_Click(object sender, RoutedEventArgs e)
        {
            String filePath = @"PATH";
            System.Diagnostics.Process.Start(filePath);

            Process wordProcess = new Process();
            wordProcess.StartInfo.FileName = filePath;
            wordProcess.StartInfo.UseShellExecute = true;
            wordProcess.Start();

            /*
            Process process = new Process();
            process.StartInfo.UseShell Execute = true;
            process.StartInfo.FileNme = _sSelected;
            process.Start();
            
        }
        //fileBrowserDiaglog 
        private void openFile_click(object sender, RoutedEventArgs e)
        {
            /*
            Process wordProcess = new Process();
            wordProcess.StartInfo.FileName = pathToYourDocument;
            wordProcess.StartInfo.UseShellExecute = true;
            wordProcess.Start();
            */
        }

        private void dataGrid1_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

        }

        private void Page_Loaded(object sender, SelectionChangedEventArgs e)
        {

        }
    }
}