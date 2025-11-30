using Cent.Core.Gamemodes;
using Cent.Core.Gamemodes.Abstractions;

Console.WriteLine("Select the difficulity (in cents):");
Console.WriteLine("(semitone - 100):");

int differenceInCentiles = GetDifficulity();
var game = new SelectTunedInterval(differenceInCentiles);
//var game = new SelectLowerSound(differenceInCentiles);
Console.WriteLine(game.Description);

int questionNumber = 1;
int correctAnswers = 0;
while (true)
{
    Console.WriteLine($"QUESTION: {questionNumber}");
    Console.WriteLine($"CORRECT: {correctAnswers}/{questionNumber-1}");

    var question = game.GetQuestion();

    PlaySound(question);

    bool isResponded = false;
    while (!isResponded)
    {
        var response = GetResponse();

        if (response is not null)
        {
            isResponded = true;
            if (question.CheckAnswer(response.Value))
            {
                Console.WriteLine("Correct!");
                correctAnswers++;
                continue;
            }
            
            HandleMistake(question);
            continue;
        }
            
        PlaySound(question);
    }

    questionNumber++;
}

int GetDifficulity()
{
    var success = false;
    int differenceInCentiles = 0;

    while (!success)
    {
        var response = Console.ReadLine();
        success = int.TryParse(response, out differenceInCentiles);
        if (!success)
            Console.WriteLine("Enter a valid number.");

        return differenceInCentiles;
    }

    return differenceInCentiles;
}

void HandleMistake(IQuestion question)
{
    var response = "";

    Console.WriteLine("Wrong!");
    Console.WriteLine("3 for next, any for repeat");

    while (response != "3")
    {
        PlaySound(question);
        response = Console.ReadLine();
    }
}

void PlaySound(IQuestion question)
{
    question.PlayFirst();
    Thread.Sleep(500);
    question.PlaySecond();
}

bool? GetResponse()
{
    var response = Console.ReadLine();

    if (response == "1")
        return true;

    if (response == "2")
        return false;

    return null;
}