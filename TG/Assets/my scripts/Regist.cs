using MySql.Data.MySqlClient;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

namespace TG
{
    public class Regist : MonoBehaviour
    {

        public InputField registUsername;
        public InputField registPassword;
        public Button regist;
        public DateTime dt;
        // Use this for initialization
        void Start()
        {
            regist = this.GetComponent<Button>();
            registUsername = GameObject.Find("user").GetComponent<InputField>();
            registPassword = GameObject.Find("password").GetComponent<InputField>();
            regist.onClick.AddListener(OnClick);

        }

        // Update is called once per frame
        void Update()
        {

        }
        public void OnClick()
        {
            dt = DateTime.Now;
            string userId;
            string pwd;
            userId = registUsername.text;
            pwd = registPassword.text;
            string conn = "server=101.132.41.141;Database=capsule tennis;Uid=root;password=abcabc;";//connect mysql on aliyun
            MySqlConnection mycon = new MySqlConnection(conn);
            mycon.Open();
            string sql = string.Format("select count(*) from user where userid='{0}' and password='{1}'", userId, pwd);//查询是否有该条记录，根据账户密码
            MySqlCommand mycmd = new MySqlCommand(sql, mycon);//sqlcommand表示要向数据库执行sql语句或存储过程
            int i = Convert.ToInt32(mycmd.ExecuteScalar());//执行后返回记录行数
            if (i > 0)//如果大于1，说明记录存在，说明用户重复
            {
                #if UNITY_EDITOR
                EditorUtility.DisplayDialog("注册", "注册失败，用户名重复", "OK");
                #endif
                userId = "";
                pwd = "";

            }
            else
            {
                //执行insert语句
                string sqlIn = string.Format("insert into user(userid,password) values('{0}','{1}')", userId, pwd);
                MySqlCommand sqlCommand = new MySqlCommand(sqlIn, mycon);
                sqlCommand.ExecuteScalar();
                string sqlInLog = string.Format("insert into user_log(userid,login_time) values('{0}','{1}')", userId, dt);
                MySqlCommand sqlCommand1 = new MySqlCommand(sqlInLog, mycon);
                sqlCommand1.ExecuteScalar();
                //int mm = sqlCommand.ExecuteNonQuery();
                PlayerPrefs.SetString("PlayerName", userId);

                SceneManager.LoadScene(3);
            }
            mycon.Close();
        }
    }
}
