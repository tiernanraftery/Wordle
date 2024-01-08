//download .net communitytoolkit if code doesnt run
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using FiveLetters.Model;

namespace Wordle.ViewModel;

public partial class GameViewModel : ObservableObject
{
    
    int rowIndex;

    int columnIndex;

    char[] correctAnswer;

    public char[] KeyboardRow1 { get; }
    public char[] KeyboardRow2 { get; }
    public char[] KeyboardRow3 { get; }

    [ObservableProperty]
    private WordRow[] rows;

    public GameViewModel()
    {
        rows = new WordRow[6]
        {
            //have 6 rows
            new WordRow(),
            new WordRow(),
            new WordRow(),
            new WordRow(),
            new WordRow(),
            new WordRow()
        };
        //use just one word for now 
        //word must be 5 letters 
        correctAnswer = "TEAMS".ToCharArray();
        KeyboardRow1 = "QWERTYUIOP".ToCharArray();
        KeyboardRow2 = "ASDFGHJKL".ToCharArray();
        KeyboardRow3 = "<ZXCVBNM>".ToCharArray();
    }

    public void Enter()
    {
        if (columnIndex != 5)
            return;
        //used to declare a variable 
        var correct = Rows[rowIndex].Validate(correctAnswer);

        if (correct)
        {
            App.Current.MainPage.DisplayAlert("Correct! come back for tommorrows Worlde ");
            return;
        }

        if (rowIndex == 5)
        {
            App.Current.MainPage.DisplayAlert("Game Over!", "You are out of turns", "Better luck next time!");
        }
        else
        {
            rowIndex++;
            columnIndex = 0;
        }
    }
    //works using the community toolkit 
    [ICommand] 
    public void EnterLetter(char letter)
    {
        if (letter == '>')
        {
            Enter();
            return;
        }

        if (letter == '<')
        {
            if (columnIndex == 0)
                return;
            columnIndex--;
            Rows[rowIndex].Letters[columnIndex].Input = ' ';

            return;
        }

        if (columnIndex == 5)
            return;

        Rows[rowIndex].Letters[columnIndex].Input = letter;
        columnIndex++;
    }

}
