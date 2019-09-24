using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CYHTools
{
    public class SettingConfig
    {
        /// <summary>
        /// 进退位类型
        /// </summary>
        public int AdavanceType { get; set; }

        /// <summary>
        /// 项数类型 0 两厢 1三项 2混合
        /// </summary>
        public int ItemType { get; set; }

        /// <summary>
        /// 最大数
        /// </summary>
        public int MaxNum { get; set; }


        /// <summary>
        /// 题目总数
        /// </summary>
        public int TotalNum { get; set; }

        /// <summary>
        /// 加法百分比
        /// </summary>
        public decimal AddPercent { get; set; }


        /// <summary>
        /// 减法百分比
        /// </summary>
        public decimal SubPercent { get; set; }

        /// <summary>
        /// 三项混合加法百分比
        /// </summary>
        public decimal MixedPercent { get; set; }

    }
}
