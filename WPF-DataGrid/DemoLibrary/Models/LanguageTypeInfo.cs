using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DemoLibrary.Model
{
    /// <summary>
    /// The class which used to store language information.
    /// </summary>
    public class LanguageTypeInfo
    {

        /// <summary>
        /// Language name to obtain the file.
        /// </summary>
        public String Tag { get; }

        /// <summary>
        /// Language name to display.
        /// </summary>
        public String Content { get; }

        /// <summary>
        /// Initializes a new instance of the <see cref="LanguageTypeInfo"/> class.
        /// </summary>
        /// <param name="tag"></param>
        /// <param name="content"></param>
        public LanguageTypeInfo(String tag, String content)
        {
            this.Tag = tag;
            this.Content = content;
        }
    }
}
