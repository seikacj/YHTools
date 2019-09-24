using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CYHTools
{
    /// <summary>
    /// 加减混合
    /// </summary>
    public class AddSubDataCreator : BaseDataCreator
    {
        private SettingConfig config;
        public AddSubDataCreator(SettingConfig config)
        {
            this.config = config;
        }
        public override DataTable CreatData()
        {
            int rowNum = (int)Math.Ceiling(config.TotalNum / 4M);//行数
            List<string> list = new List<string>();

            string path = AppDomain.CurrentDomain.BaseDirectory + @"\100以内加减";
            if (!Directory.Exists(path))
            {
                Directory.CreateDirectory(path);
            }
            using (FileStream stream = new FileStream(path + @"\题目" + DateTime.Now.ToString("yyMMddHHmmss") + ".csv", FileMode.Create))
            {

                int max = 0;//第一位
                int min = 0;//第一位
                int min1 = 0;
                int min2 = 0;
                string item = "";
                //加法
                for (int i = 0; i < config.AddPercent * config.TotalNum; i++)
                {

                    if (this.config.MaxNum == 10)
                    {
                        max = new Random(Guid.NewGuid().GetHashCode()).Next(0, 10);
                        min = new Random(Guid.NewGuid().GetHashCode()).Next(0, 10 - max);
                        item = string.Format("{0}-{1}=", max, min);
                        list.Add(item);
                        continue;
                    }
                    item = "";
                    int count = 0;//重复次数计数器
                    //防止题目重复 5次后强制退出
                    while ((string.IsNullOrEmpty(item) || (list.Contains(item) && (config.AdavanceType != 1))) && (count < 5))
                    {
                        switch (config.AdavanceType)
                        {
                            case 1:
                                max = new Random(Guid.NewGuid().GetHashCode()).Next(1, config.MaxNum - 10);
                                min1 = new Random(Guid.NewGuid().GetHashCode()).Next((config.MaxNum - max) % 10, 10);
                                min2 = new Random(Guid.NewGuid().GetHashCode()).Next(0, (config.MaxNum - max) / 10) * 10;
                                min = min1 + min2;
                                break;
                            case 2:
                                max = new Random(Guid.NewGuid().GetHashCode()).Next(10, config.MaxNum);
                                min1 = new Random(Guid.NewGuid().GetHashCode()).Next(1, (((config.MaxNum - max) % 10) == 0) ? 10 : ((config.MaxNum - max) % 10));
                                min2 = new Random(Guid.NewGuid().GetHashCode()).Next(0, (config.MaxNum - max) / 10) * 10;
                                min = min1 + min2;
                                break;
                            default:
                                max = new Random(Guid.NewGuid().GetHashCode()).Next(10, config.MaxNum);
                                min = new Random(Guid.NewGuid().GetHashCode()).Next(1, config.MaxNum - max);
                                break;
                        }
                        item = string.Format("{0}+{1}=", max, min);
                        list.Add(item);
                        count++;
                    }
                }

                //减法
                for (int i = 0; i < config.SubPercent * config.TotalNum; i++)
                {
                    if (config.MaxNum == 10)
                    {
                        max = new Random(Guid.NewGuid().GetHashCode()).Next(0, 10);
                        min = new Random(Guid.NewGuid().GetHashCode()).Next(0, max);
                        item = string.Format("{0}-{1}=", max, min);
                        list.Add(item);
                        continue;
                    }

                    item = "";
                    int count = 0;//重复次数计数器
                    //防止题目重复 5次后强制退出
                    while ((string.IsNullOrEmpty(item) || (list.Contains(item) && (config.AdavanceType != 1))) && (count < 5))
                    {
                        switch (config.AdavanceType)
                        {
                            case 1:
                                max = new Random(Guid.NewGuid().GetHashCode()).Next(10, config.MaxNum);
                                min1 = new Random(Guid.NewGuid().GetHashCode()).Next((max % 10) + 1, 10);
                                min2 = new Random(Guid.NewGuid().GetHashCode()).Next(0, max / 10) * 10;
                                min = min1 + min2;
                                break;

                            case 2:
                                max = new Random(Guid.NewGuid().GetHashCode()).Next(10, config.MaxNum);
                                min1 = new Random(Guid.NewGuid().GetHashCode()).Next(1, ((max % 10) == 0) ? 10 : (max % 10));
                                min2 = new Random(Guid.NewGuid().GetHashCode()).Next(0, max / 10) * 10;
                                min = min1 + min2;
                                break;

                            default:
                                max = new Random(Guid.NewGuid().GetHashCode()).Next(10, config.MaxNum);
                                min = new Random(Guid.NewGuid().GetHashCode()).Next(1, max);
                                break;
                        }
                        item = string.Format("{0}-{1}=", max, min);
                        list.Add(item);
                        count++;
                    }
                }


                //三项混合
                for (int i = 0; i < config.MixedPercent * config.TotalNum; i++)
                {
                    int posstion = new Random(Guid.NewGuid().GetHashCode()).Next(0, 3);//未知数位置
                    int result = 0;
                    int first = new Random(Guid.NewGuid().GetHashCode()).Next(1, config.MaxNum);
                    int operator1 = new Random(Guid.NewGuid().GetHashCode()).Next(0, 2);//加减符号
                    int second = operator1 == 0 ? new Random(Guid.NewGuid().GetHashCode()).Next(0, config.MaxNum - first) : new Random(Guid.NewGuid().GetHashCode()).Next(0, first);
                    result = first + (operator1 == 0 ? second : -second);
                    int operator2 = new Random(Guid.NewGuid().GetHashCode()).Next(0, 2);//加减符号
                    int third = operator2 == 0 ? new Random(Guid.NewGuid().GetHashCode()).Next(0, config.MaxNum - result) : new Random(Guid.NewGuid().GetHashCode()).Next(0, result);
                    result = result + (operator2 == 0 ? third : -third);

                    switch (posstion)
                    {
                        case 0:
                            item = string.Format("{0}{1}{2}{3}{4}={5}", "(  )", operator1 == 0 ? "+" : "-", second, operator2 == 0 ? "+" : "-", third, result);
                            break;
                        case 1:
                            item = string.Format("{0}{1}{2}{3}{4}={5}", first, operator1 == 0 ? "+" : "-", "(  )", operator2 == 0 ? "+" : "-", third, result);
                            break;
                        case 2:
                            item = string.Format("{0}{1}{2}{3}{4}={5}", first, operator1 == 0 ? "+" : "-", second, operator2 == 0 ? "+" : "-", "(  )", result);
                            break;
                        default:
                            break;
                    }

                    list.Add(item);

                }

                List<string> list2 = (from c in list
                                      orderby Guid.NewGuid()
                                      select c).ToList<string>();

                using (StreamWriter writer = new StreamWriter(stream))
                {
                    int count = 0;//题目计数器
                    for (int i = 0; i < rowNum; i++)
                    {
                        for (int j = 0; j < 4; j++)
                        {
                            if (count < config.TotalNum)
                            {
                                writer.Write(list2[count] + ",");
                            }
                            count++;
                        }
                        writer.Write("\r\n");
                    }
                }

                DataTable table = new DataTable();
                table.Columns.Add("列1");
                table.Columns.Add("列2");
                table.Columns.Add("列3");
                table.Columns.Add("列4");
                for (int i = 0; i < rowNum; i++)
                {
                    DataRow row = table.NewRow();
                    row[0] = ((i * 4) < config.TotalNum) ? list2[i * 4] : "";
                    row[1] = (((i * 4) + 1) < config.TotalNum) ? list2[(i * 4) + 1] : "";
                    row[2] = (((i * 4) + 2) < config.TotalNum) ? list2[(i * 4) + 2] : "";
                    row[3] = (((i * 4) + 3) < config.TotalNum) ? list2[(i * 4) + 3] : "";
                    table.Rows.Add(row);
                }

                return table;
            }

        }
    }
}
