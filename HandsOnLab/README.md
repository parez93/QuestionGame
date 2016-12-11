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
