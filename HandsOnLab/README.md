## Xamarin Forms Hands On Lab

Oggi, impareremo a creare un'applicazione [Xamarin.Forms](http://xamarin.com/forms) che porrà delle domande all'utente e esso dovrà rispondere. Al termine veràà mostrato il numero di risposte corrette.
Toccheremo con mano sia alcuni aspetti di come si costruiscono le intefacce grafiche in XAML sia aspetti riguardanti la business logic.

### Get Started

Open **Start/QuestionGame.sln**

This solution contains 2 projects

* QuestionGame  - Shared Project that will have all shared code.
* QuestionGame.Droid - Xamarin.Android application

#### NuGet Restore

All projects have the required NuGet packages already installed, so there will be no need to install additional packages during the Hands on Lab. The first thing that we must do is restore all of the NuGet packages from the internet.

This can be done by **Right-clicking** on the **Solution** and clicking on **Restore NuGet packages...**

### QuestionPage.xaml
Andiamo a definire quella che sarà la grafica della pagina “QuestionPage”.

Per prima cosa andiamo a dividere la schermata in una griglia: una divisione verticale della schermata e 4 suddivisioni orizzontali.

Creiamo dunque la struttura della griglia:

```xml
<Grid Padding="20" ColumnSpacing="10" RowSpacing="20" AutomationId="questionPage">

    <Grid.RowDefinitions>
      <RowDefinition Height="auto" />
      <RowDefinition Height="auto" />
      <RowDefinition Height="*"    />
      <RowDefinition Height="auto" />
    </Grid.RowDefinitions>

    <Grid.ColumnDefinitions>
      <ColumnDefinition />
      <ColumnDefinition />
    </Grid.ColumnDefinitions>

  </Grid>
```

Vengono definite tante righe quante sono i tag `<RowDefinition>` figli del tag `<Grid.RowDefinitions>`. Allo stesso modo viene fatto per le colonne.

Impostare la proprietà Height a `“auto”` significa essenzialmente che l’altezza di una riga è data dall’altezza degli elementi che la compongono.

Impostare la proprietà Height a `“*”` significa che l’altezza di una riga è proporzionale alle dimensioni della griglia.

Nel caso delle colonne non è stato specificato nulla, questo perché sarebbe implicito `Width="*"` per entrambe le colonne in quanto desideriamo che lo spazio occupato da ciascuna sia proporzionale.

Andiamo quindi ad inserire una serie di etichette e bottoni all’interno del tag `<Grid>` nel seguente modo:

```xml
    <Label x:Name="lblQuestion"
      	AutomationId="questionText"
	      Grid.ColumnSpan="2"
	      Text="Question" />

    <Button x:Name="btnTrue"
	        AutomationId="trueButton"
      	  Grid.Row="1"
	        Text="True"
	        TextColor="White"
	        BackgroundColor="#0892D0">
    </Button>

    <Button x:Name="btnFalse"
	      AutomationId="falseButton"
      	Grid.Row="1"
	      Grid.Column="1"
	      Text="False"
	      TextColor="White"
	      BackgroundColor="#D0082E">
    </Button>

    <Label x:Name="lblResponse"
	      AutomationId="resultText"
      	Grid.Row="2"
	      Grid.RowSpan="2"
	      Text="Response" />

    <Button x:Name="btnNext"
	      AutomationId="nextButton"
      	Grid.Row="3"
	      Grid.Column="1"
	      Text="Next"
	      TextColor="White"
	      BackgroundColor="#08D046">
    </Button>
```

Con `Grid.Row="1"` e `Grid.Column="1"` andiamo a specificare in quale riga e colonna gli elementi devono essere inseriti. Il conto parte da 0, ciò significa che la prima riga/colonna è la riga/colonna numero zero.

Il risultato finale dovrebbe essere il seguente:

```xml
<?xml version="1.0" encoding="utf-8" ?>
<ContentPage xmlns="http://xamarin.com/schemas/2014/forms"
             xmlns:x="http://schemas.microsoft.com/winfx/2009/xaml"
             x:Class="QuestionGame.QuestionPage"
             Title="QuestionGame">
  
  <Grid Padding="20" ColumnSpacing="10" RowSpacing="20" AutomationId="questionPage">

    <Grid.RowDefinitions>
      <RowDefinition Height="auto" />
      <RowDefinition Height="auto" />
      <RowDefinition Height="*"    />
      <RowDefinition Height="auto" />
    </Grid.RowDefinitions>

    <Grid.ColumnDefinitions>
      <ColumnDefinition />
      <ColumnDefinition />
    </Grid.ColumnDefinitions>

        <Label x:Name="lblQuestion"
      	AutomationId="questionText"
	Grid.ColumnSpan="2"
	Text="Question" />

    <Button x:Name="btnTrue"
	AutomationId="trueButton"
      	Grid.Row="1"
	Text="True"
	TextColor="White"
	BackgroundColor="#0892D0">
    </Button>

    <Button x:Name="btnFalse"
	AutomationId="falseButton"
      	Grid.Row="1"
	Grid.Column="1"
	Text="False"
	TextColor="White"
	BackgroundColor="#D0082E">
    </Button>

    <Label x:Name="lblResponse"
	AutomationId="resultText"
      	Grid.Row="2"
	Grid.RowSpan="2"
	Text="Response" />

    <Button x:Name="btnNext"
	AutomationId="nextButton"
      	Grid.Row="3"
	Grid.Column="1"
	Text="Next"
	TextColor="White"
	BackgroundColor="#08D046">
    </Button>

  </Grid>
</ContentPage>
```

### QuizQuestion.cs

Andiamo ora a creare dei getter e setter per poter incapsulare i nostri dati:

```csharp
namespace QuestionGame
{
    public class QuizQuestion
    {
        public string Id { get; set; }
        public string Question { get; set; }
        public bool Answer { get; set; }
        public string Explanation { get; set; }

    }
}
```

### QuestionPage.cs

#### Metodo ResetUI

In questo metodo andiamo a resettare la grafica dell’applicazione caricando una nuova domanda, abilitando i bottoni Vero e Falso e disabilitando il bottone Next finchè l’utente non avrà dato la risposta.

Il codice è il seguente:

```csharp
        void ResetUI()
        {
            lblQuestion.Text = game.CurrentQuestion.Question;
            lblResponse.Text = String.Empty;

            btnTrue.IsEnabled = true;
            btnFalse.IsEnabled = true;
            btnNext.IsEnabled = false;
        }
```

#### Metodo OnAnswer

In questo metodo salviamo le risposte dell’utente ai vari quesiti e visualizziamo se la risposta è corretta o sbagliata. 

Infine abilitiamo il tasto Next per passare alla domanda successiva e disabilitiamo i tasti Vero e Falso.

L’implementazione è la seguente:

```csharp
        void OnAnswer(bool answer)
        {
            if (answer == true)
                game.OnTrue();
            else
                game.OnFalse();

            lblResponse.Text = game.CurrentResponse == game.CurrentQuestion.Answer ? "Correct" : "Incorrect";

            btnTrue.IsEnabled = false;
            btnFalse.IsEnabled = false;
            btnNext.IsEnabled = true;
        }
```

#### Metodo OnNextClicked
Questo metodo definisce come l’applicazione deve comportarsi nel caso si prema il bottone Next. 

Gli scenari sono 2: il quiz non è finito perché ci sono altre domande da sottomettere all’utente e quindi è necessario aggiornare la grafica con una nuova domanda; nel secondo caso l’utente ha risposto a tutte le domande e quindi bisogna navigare verso una nuova Pagina (ReviewPage) in cui viene mostrato il numero di risposte corrette.

L’implementazione è la corrente:

```csharp
        void OnNextClicked(object sender, EventArgs e)
        {
            if (game.NextQuestion() == true)
            {
                ResetUI();
            }
            else
            {
                this.Navigation.PushAsync(new ReviewPage(game));
            }
        }
```

#### ReviewPage.xaml
In questo file andiamo a creare un’etichetta all’interno della “ContentPage” che ci mostrerà il numero di risposte corrette date dall’utente. 

Il codice è il seguente:

```xml
  <Label x:Name="lblResult" 
      VerticalOptions="Center" 
      HorizontalOptions="Center" 
      AutomationId="resultText" />
```

#### ReviewPage.xaml
Andiamo quindi a recuperare il numero di risposte corrette e il numero di domande e ad aggiornare la grafica.

```csharp
public ReviewPage(Game game)
        {
            InitializeComponent();

lblResult.Text = String.Format("Great! You got {0} out of {1} questions correct", game.GetNumberOfCorrectResponses(), game.NumberOfQuestions);

        }

```







