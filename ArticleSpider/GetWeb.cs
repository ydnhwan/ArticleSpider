using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace ArticleSpider
{
        enum ThreadState { Running=0, New=1, Runnable=2, Blocked=3, Dead=4};
        class GetWeb
        {
                private string        basicUrl;
                /// <summary>
                /// 布隆过滤器，用来检查url是否重复
                /// </summary>
                private static BloomFilter bloomfilter;
                private static object lockThis = new object(); 

                /// <summary>
                /// 设置起始URL
                /// </summary>
                public string BasicUrl
                {
                        get { return basicUrl; }
                        set { basicUrl = value; }
                }

                public GetWeb()
                {
                        bloomfilter = new BloomFilter(2000000000); 
                }
                /// <summary>
                /// 开始执行分析
                /// </summary>
                public virtual void Start()
                {
                        HttpWebResponse hwr = HttpHelper.CreateGetHttpResponse(basicUrl,
                     3000, "Mozilla/5.0 (Windows NT 6.1; Trident/7.0; rv:11.0) like Gecko", "", null, null);
                        string res = HttpHelper.GetResponseString(hwr);
                        Console.WriteLine(res);
                }

                /// <summary>
                /// 开始分析单个网页
                /// </summary>
                /// <param name="url"></param>
                /// <returns></returns>
                private bool StartSingleWeb(string url)
                {
                        bool isContant = false;
                        lock(lockThis)
                        {
                                isContant = bloomfilter.Contains(url);
                        }
                        if (isContant)
                                return false;


                        lock(lockThis)
                        {
                                bloomfilter.Add(url);
                        }
                        return true;
                }
             

        }
}
