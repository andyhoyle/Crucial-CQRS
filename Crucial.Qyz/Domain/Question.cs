using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Crucial.Qyz.Domain
{

    public class QuestionWithAnswers : Question
    {
        public QuestionWithAnswers(Question question)
        {
            QuestionText = question.QuestionText;
        }
        
        public QuestionAnswers Answers { get; set; }
    }

    public class QuestionAnswerTypeText : QuestionAnswer<string>
    {

    }

    public class QuestionAnswerTypePicture : QuestionAnswer<byte[]>
    {

    }

    public class QuestionAnswerTypeNumeric : QuestionAnswer<int>
    {

    }

    public class QuestionAnswers
    {
        public Dictionary<QuestionAnswer, bool> Answers { get; set; }
    }

}
