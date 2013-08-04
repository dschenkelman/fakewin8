fakewin8
========
Provides a set of components that aim to simplify the creation of fakes to unit test Windows Store applications.

Premises
---------
The following are the core premises followed by **fakewin8**:

1. For each interface or base class, create **only one class that can be used as a stub/mock in any test method**. This means that all methods must be easily setup in different unit tests.
1. No unnecessary components should be necessary (i.e.: no portable class libraries, no linked files).
1. Method invocations should be automatically tracked so assertions can be performed based on them.
1. Fake class generation should be automatic. We want to focus on the tests development, not the fakes development.

Proposal
--------
**fakewin8** proposes the usage of classes like `FakeAction` and `FakeFunc`, which act as normal `Action`s and `Func`s, but keep track of the number invocations and parameters on each of them.

For example, for this interface:
```CSharp
public interface INavigationService
{
    void Navigate(string view);

    void GoBack();
}
```

The following fake class should be created:
```CSharp
public class FakeNavigationService : INavigationService
{
    public FakeAction<string> NavigateAction { get; set; }

    public FakeAction GoBackAction { get; set; }

    public void Navigate(string viewName)
    {
        this.NavigateAction.Invoke(viewName);
    }

    public void GoBack()
    {
        this.GoBackAction.Invoke();
    }
}
```

The `FakeAction` and `FakeFunc` classes (at the moment they support until up to 3 parameters) can be leveraged like this in your unit tests:
```CSharp
// arrange
this.fakeNavigationService.NavigateAction = FakeMethodFactory.CreateAction<string>(view => { });

// act

// assert
Assert.AreEqual(1, this.fakeNavigationService.NavigateAction.NumberOfInvocations);
Assert.AreEqual("ViewName", this.fakeNavigationService.NavigateAction.Invocations.ElementAt(0).FirstParameter);
```

Additionally, given the path to an assembly and an output directory, you can automatically generate the fake classes.
```Shell
FakeWin8.Generator.Console.exe <dllPath> <outputDir>
```