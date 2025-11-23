using Cent.Core.Gamemodes;

var game = new SelectLowerSound();

Console.WriteLine("Select the lower sound!");
Console.WriteLine("1 - pick first sound");
Console.WriteLine("2 - pick second sound");
Console.WriteLine("Any buttom - repeat question");

while (true)
{
    var question = game.GetQuestion();

    question.PlayFirstNote();
    Thread.Sleep(500);
    question.PlaySecondNote();

    bool isResponded = false;
    while (!isResponded)
    {
        var response = Console.ReadLine();
        switch (response)
        {
            case "1":
                isResponded = true;
                if (question.CheckAnswer(true))
                {
                    Console.WriteLine("Correct!");
                }
                else
                {
                    Console.WriteLine("Wrong!");
                }
                break;
            case "2":
                isResponded = true;
                if (question.CheckAnswer(false))
                {
                    Console.WriteLine("Correct!");
                }
                else
                {
                    Console.WriteLine("Wrong!");
                }
                break;
            default:
                question.PlayFirstNote();
                Thread.Sleep(500);
                question.PlaySecondNote();
                break;
        }
    }
}