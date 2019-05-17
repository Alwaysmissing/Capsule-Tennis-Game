using MySql.Data.MySqlClient;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using UnityEditor;

namespace TG
{

    public class Login : MonoBehaviour
    {

        public InputField userName;
        public InputField passWord;
        public Button confirm;
        public DateTime dt;
        // Use this for initialization
        void Start()
        {
            confirm = this.GetComponent<Button>();
            userName = GameObject.Find("user").GetComponent<InputField>();
            passWord = GameObject.Find("password").GetComponent<InputField>();
            confirm.onClick.AddListener(OnClick);
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
            userId = userName.text;
            pwd = passWord.text;
            string conn = "server=101.132.41.141;Database=capsule tennis;Uid=root;password=abcabc;";//connect mysql on aliyun
            MySqlConnection mycon = new MySqlConnection(conn);
            mycon.Open();
            string sql = string.Format("select count(*) from user where userid='{0}' and password='{1}'", userId, pwd);//查询是否有该条记录，根据账户密码        
            MySqlCommand mycmd = new MySqlCommand(sql, mycon);//sqlcommand表示要向数据库执行sql语句或存储过程

            int i = Convert.ToInt32(mycmd.ExecuteScalar());//执行后返回记录行数
            if (i > 0)//如果大于1，说明记录存在，登录成功
            {
                string sqlDt = string.Format("update user_log set login_time='{0}' where userid='{1}';", dt, userId);
                MySqlCommand mySqlcmd = new MySqlCommand(sqlDt, mycon);
                mySqlcmd.ExecuteScalar();
                PlayerPrefs.SetString("PlayerName", userId);
                SceneManager.LoadScene(3);
            }

            else
            {
                #if UNITY_EDITOR
                EditorUtility.DisplayDialog("登录", "登录失败，请检查用户名或密码", "OK");
                #endif
                pwd = "";
            }
            mycon.Close();
        }
    }
}
