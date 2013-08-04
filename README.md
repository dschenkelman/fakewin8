fakewin8
========
Provides a set of components that aim to simplify the creation of fakes to unit test Windows Store applications.

Proposal
---------
The following are the core premises followed by **fakewin8**:

1. For each interface or base class, create **only one class that can be used as a stub/mock in any test method**. 
1. For each public abstract method, a public property of a delegate type that matches the method's signature must exist in the fake class.
1. Method invocations should be automatically tracked so assertions can be performed based on them.
1. Fake class generation should be automatic.

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

**fakewin8** provides the `FakeAction` and `FakeFunc` classes (at the moment they support until up to 3 parameters), which can be leveraged like this:
```CSharp
// arrange
this.fakeNavigationService.NavigateAction = FakeMethodFactory.CreateAction<string>(view => { });

// act

// assert
Assert.AreEqual(1, this.fakeNavigationService.NavigateAction.NumberOfInvocations);
Assert.AreEqual("ViewName", this.fakeNavigationService.NavigateAction.Invocations.ElementAt(0).FirstParameter);
```

Additionally, given the path to an assembly and an output directory, you can automatically generate the Fake classes.