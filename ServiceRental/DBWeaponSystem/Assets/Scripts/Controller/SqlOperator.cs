using Assets.Scripts.Model;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace Assets.Scripts
{
    static class SqlOperator
    {
        const string constr =
            "Data Source=127.0.0.1,1433;" +
            "Initial Catalog=seviceRental;" +
            "User Id=DB;" +
            "Password =123456;" +
            "Integrated Security=false;";
        //数据库连接
        static public SqlConnection SqlConnection;

        //静态构造初始化
        static SqlOperator() { ConnectSql(); }

        //连接数据库
        static private void ConnectSql()
        {

            SqlConnection = new SqlConnection(constr);

        }

        //打开连接数据库
        static public bool OpenSql()
        {
            try
            {
                //关闭的时候才开，否则抛异常
                if (SqlConnection.State == System.Data.ConnectionState.Closed)
                    SqlConnection.Open();
                Debug.Log("成功打开数据库");
                return true;
            }
            catch (Exception e) { Debug.Log("打开数据库 失败" + e.ToString()); }
            return false;
        }

        //关闭连接数据库
        static public void CloseSql()
        {
            try
            {
                if (SqlConnection.State == System.Data.ConnectionState.Open)
                    SqlConnection.Close();
                Debug.Log("成功关闭数据库");
            }
            catch (Exception e) { Debug.Log("关闭数据库 失败" + e.ToString()); }
        }

        //是否存在帐号或帐号密码匹配
        static public bool IsMatchAccount(string userName, string password = null)
        {
            if (!OpenSql())
                return false;

            bool isMatch = false;

            StringBuilder commandStr = new StringBuilder();
            commandStr.Append(" Select * From userlist");
            commandStr.Append(" Where uname = '" + userName + "'");

            if (!string.IsNullOrEmpty(password))                        //加了密码就是检测帐号密码同时匹配
                commandStr.Append(" And upassword = '" + password + "'");

            try
            {
                SqlCommand sqlCommand = new SqlCommand(commandStr.ToString(), SqlConnection);
                SqlDataReader reader = sqlCommand.ExecuteReader();
                if (reader.Read())
                {
                    isMatch = true;
                    Debug.Log("查询用户存在、或账户密码匹配成功");
                }

            }
            catch (Exception e) { Debug.Log("查询用户存在 失败" + e.ToString()); }
            finally { CloseSql(); }

            return isMatch;
        }

        /// <summary>
        /// 添加用户
        /// </summary>
        /// <param name="uname"></param>
        /// <param name="upassword"></param>
        /// <param name="utype">添加对应账户类型表：0=customer,1=freer,2=manager</param>
        /// <param name="utellphone"></param>
        /// <param name="uprofession"></param>
        /// <param name="clevel"></param>
        /// <returns></returns>
        static public bool AddUser(string uname, string upassword,int utype,string utellphone,string uprofession,string clevel="0")
        {
            if (IsMatchAccount(uname) || !OpenSql())          //已存在该帐号
                return false;

            string uno = DateTime.Now.TimeOfDay.ToString();

            //添加user表
            StringBuilder commandStr = new StringBuilder();
            commandStr.Append(" Insert Into userlist(uno,upassword,uname,utype) Values('" + uno + "','"+ upassword + "','" + uname + "','" + utype +"')");

            SqlCommand sqlCommand = new SqlCommand(commandStr.ToString(), SqlConnection);
            try
            {
                sqlCommand.ExecuteScalar();
                Debug.Log(string.Format("成功添加用户 {0} ,密码 {1}", uname, upassword));
            }
            catch (Exception e) { Debug.Log("添加用户 失败了啊" + e.ToString()); }


            //添加对应账户类型表：customer,freer,manager
            StringBuilder commandStr2 = new StringBuilder();
            switch(utype)
            {
                case 0:
                    commandStr2.Append(" Insert Into customer(cno,cname,clevel,ctellphone) Values('" + uno + "','" + uname + "','" + clevel+ "','" + utellphone + "')");
                    break;
                case 1:
                    commandStr2.Append(" Insert Into freer(fno,fname,ftype,ftellphone) Values('" + uno + "','" + uname + "','" + uprofession + "','" + utellphone + "')");
                    break;
                case 2:
                    commandStr2.Append(" Insert Into manager(mno,mname,mtellphone) Values('" + uno + "','" + uname + "','" + utellphone + "')");
                    break;
            }
            
            SqlCommand sqlCommand2 = new SqlCommand(commandStr2.ToString(), SqlConnection);
            try
            {
                sqlCommand2.ExecuteScalar();
                Debug.Log(string.Format("成功添加对应表用户 {0} ,密码 {1}", uname, upassword));
            }
            catch (Exception e) { Debug.Log("添加对应表用户 失败了啊" + e.ToString()); }

            finally { CloseSql(); }



            return true;
        }

        //删除用户
        static public bool DeleteAccount(string userName)
        {
            if (!IsMatchAccount(userName) || !OpenSql())         //不存在该账户
                return false;

            StringBuilder commandStr = new StringBuilder();
            commandStr.Append("DELETE FROM userlist WHERE uname = '" + userName + "'");

            SqlCommand sqlCommand = new SqlCommand(commandStr.ToString(), SqlConnection);
            try
            {
                sqlCommand.ExecuteScalar();
                Debug.Log(string.Format("删除用户 {0}", userName));
            }
            catch (Exception e) { Debug.Log("删除用户 失败了啊" + e.ToString()); }
            finally { CloseSql(); }

            return true;
        }

        //获取用户列表
        static public List<UserModel> GetUserList()
        {
            List<UserModel> userList = new List<UserModel>();

            if (!OpenSql())                 //打开数据库失败返回空的用户列表。
                return userList;

            try
            {
                SqlCommand sqlCommand = new SqlCommand("Select * from userlist", SqlConnection);
                SqlDataReader reader = sqlCommand.ExecuteReader();
                while (reader.Read())
                    userList.Add(new UserModel(reader["uname"].ToString(), reader["upassword"].ToString()));
                Debug.Log("成功查询获取整个用户表");
            }
            catch (Exception e) { Debug.Log("查询获取整个用户表 失败了啊 " + e.ToString()); }
            finally { CloseSql(); }

            return userList;
        }

        /// <summary>
        /// 查询SQL
        /// </summary>
        /// <param name="tableName">目标表名</param>
        /// <param name="notice">输出表说明文字</param>
        /// <param name="conditions">条件</param>
        /// <param name="target">目标</param>
        /// <returns></returns>
        public static List<string> SelectSQL(string tableName,string notice=null,
                                            string conditions=null,string target=null)
        {
            List<string> readSql = new List<string>();
            if (notice != null)
            {
                readSql.Add(notice);
            }
            string str = "";
            string finalRow = "";
            string commandStr = "";
            //数据库操作语句
            if (target != null)
            {
                commandStr+= "select "+target+" from " + tableName;
            }
            else
            {
                commandStr += "select * from " + tableName;
            }
            

            if(conditions!=null)
            {
                commandStr+=(" Where " + conditions);
            }
            Debug.Log(commandStr);
            //数据库操作
            SqlDataAdapter sda = new SqlDataAdapter(commandStr, constr);
            //结果集
            DataSet ds = new DataSet();
            //将查询的结果放入结果集
            sda.Fill(ds, "find");
            foreach (DataTable dt in ds.Tables)
            {

                foreach (DataRow dr in dt.Rows)
                {
                    foreach (DataColumn dc in dr.Table.Columns)
                    {
                        str += ((dr[dc]));
                        foreach (char c in str)
                        {
                            if (c != ' ') finalRow += c;
                        }
                        str = "";
                        finalRow += "  ";
                    }
                    Debug.Log(finalRow);
                    readSql.Add(finalRow);
                    finalRow = "";
                }
            }
            return readSql;
        }

     
        /// <summary>
        /// 更新SQL
        /// </summary>
        /// <param name="tableName">目标表</param>
        /// <param name="valuesChange">更改内容:列名=值，。。。</param>
        /// <param name="conditions">条件</param>
        /// <returns></returns>
        public static bool UpdateSQL(string tableName, string valuesChange, string conditions )
        {
            if (!OpenSql())       
                return false;

            StringBuilder commandStr = new StringBuilder();
            commandStr.Append(" update "+tableName+" set " + valuesChange + " where "+conditions);

            Debug.Log(commandStr);

            SqlCommand sqlCommand = new SqlCommand(commandStr.ToString(), SqlConnection);
            try
            {
                sqlCommand.ExecuteScalar();
                Debug.Log(string.Format("成功修改"));
            }
            catch (Exception e) { Debug.Log("修改失败" + e.ToString()); }
            finally { CloseSql(); }
            return true;

        }

        /// <summary>
        /// 插入SQL
        /// </summary>
        /// <param name="tableName">目标表名</param>
        /// <param name="values">插入值</param>
        /// <param name="valuesRange">指定插入值(默认null，即全插入)</param>
        /// <returns></returns>
        public static bool InsertSQL(string tableName, string values,string valuesRange=null)
        {
            if (!OpenSql())
                return false;

            StringBuilder commandStr = new StringBuilder();
            if(valuesRange!=null)
            {
                commandStr.Append(" Insert Into " + tableName + "(" + valuesRange + ")" +" Values(" + values + ")");
            }
            else
            {
                commandStr.Append(" Insert Into " + tableName + " Values(" + values + ")");
            }
            Debug.Log(commandStr);

            SqlCommand sqlCommand = new SqlCommand(commandStr.ToString(), SqlConnection);
            try
            {
                sqlCommand.ExecuteScalar();
                Debug.Log(string.Format("成功添加" + values));
            }
            catch (Exception e) { Debug.Log("添加失败" + e.ToString()); }
            finally { CloseSql(); }
            return true;

        }

        public static bool DeleteSQL(string tableName,string conditions)
        {
            if (!OpenSql())
                return false;

            StringBuilder commandStr = new StringBuilder();
            commandStr.Append(" delete from  " + tableName + " where " + conditions );

            SqlCommand sqlCommand = new SqlCommand(commandStr.ToString(), SqlConnection);
            try
            {
                sqlCommand.ExecuteScalar();
                Debug.Log(string.Format("成功删除"));
            }
            catch (Exception e) { Debug.Log("删除失败" + e.ToString()); }
            finally { CloseSql(); }
            return true;
        }

        public static string GetValueFromSQL(string tableName,string target,string conditions,string columnName)
        {
            List<string> readSql = new List<string>();

            string tempstr="";
            string str = "";
            string finalRow = "";
            string commandStr = "";
            //数据库操作语句
            commandStr += "select " + target + " from " + tableName;
            commandStr += (" Where " + conditions);


            //数据库操作
            SqlDataAdapter sda = new SqlDataAdapter(commandStr, constr);
            //结果集
            DataSet ds = new DataSet();
            //将查询的结果放入结果集
            sda.Fill(ds, "find");
            foreach (DataTable dt in ds.Tables)
            {

                foreach (DataRow dr in dt.Rows)
                {
                    foreach (DataColumn dc in dr.Table.Columns)
                    {
                        str += ((dr[dc]));
                        foreach (char c in str)
                        {
                            if (c != ' ')
                            {
                                finalRow += c;
                            }   
                            else
                            {
                                if(dc.ToString()==columnName)
                                {
                                    tempstr = finalRow;
                                }
                                else
                                {
                                    finalRow = "";
                                }
                            }
                        }
                        str = "";
                    }
                    Debug.Log(finalRow);
                    finalRow = "";
                }
            }
            return tempstr;
        }
    }


}
