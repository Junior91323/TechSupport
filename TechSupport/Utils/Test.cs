using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using System.Web;

namespace TechSupport.Utils
{
    public class Test1
    {
        public void Update()
        {
            TestTimer tt = new TestTimer("Database","request",5);
            tt.TestEH += Tt_TestEH;
            tt.Start(10);
        }

        private void Tt_TestEH(object sender, EventArgs e)
        {
            
        }
    }
    public class TestTimer
    {

        string DB;
        string _Request;
        int _Employee;

        public event EventHandler TestEH;

        public TestTimer(string db, string request, int employee)
        {
            this._Request = request;
            this._Employee = employee;
            this.DB = db;
        }
        public void Start(int interval)
        {
            Task.Factory.StartNew(() =>
            {
                System.Threading.Thread.Sleep(10000);
                TheWork();
            });

        }
        private void TheWork()
        {
            try
            {
                TestEH?.Invoke(this, new EventArgs());
            }
            catch (Exception ex)
            {
                //LOG
            }
        }
    }
}