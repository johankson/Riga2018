# Xamarin.Forms Workshop - Riga 2018
During this workshop we will create a photo app that fetches photos from Flickr that are tagged with tretton37.

To build the app we will use Xamarin.Forms. 

## Exercise 1 - Photo feed

1. Clone this repository in Visual Studio 2018 or Visual Studio for Mac.   
    ```git clone https://github.com/johankson/Riga2018.git```
2. Check out start branch.

    ```git checkout start```

1. Open LeetPhotos.sln in Visual Studio
1. Create a new project.

    ```Multiplatform -> App -> Blank Forms App```
1. Click Next.
1. Configure your app
    
    ```csharp
        AppName = "LeetPhotos"; 
        OrganizationIdentifier = "com.tretton37";
    ``` 
1. Click Next.

    > This will create two platform specific projects (one for iOS and one for Android) and a shared project that is compiled both iOS and Android on build time. The shared project can only use libraries that both environments have referenced. So if you want to be able to use a nuget-package, you need to add it to both iOS and Android.

1. Click Create.
1. Select either the iOS- or Android project as startup and run the app.
1. Gratz! You have created your first native cross platform app.

    > The app is compiled ahead of time (AOT) on iOS which will restrict some usage of reflection. You cannot generate and execute code in iOS dynamically because of this.
    >
    > On Android the code is compiled to IL and just-in-time (JITted) compiled by the .net runtime. 

1. Add references to LeetPhotos.Core on both iOS- and Android project.
1. Install ```Autofac```, ```Newtonsoft.Json``` and ```TinyNavigationHelper.Forms``` on both iOS and Android project.

    > Autofac takes care of inversion of control (IoC) and handles you container.
    > Newtonsoft handles all you json worries (serialization/deserialization),
    > TinyNavigationHelper.Forms abstracts navigation (jumping between views) from Xamarin.Forms

1. Create a new class in the LeetPhotos project and name it AutofacResolver.
1. Paste the code below:
    ```csharp
    public class AutofacResolver : IResolver
    {
		private IContainer container;

		public AutofacResolver(IContainer container)
            {
			this.container = container;
            }

		public T Resolve<T>()
		{
			return container.Resolve<T>();
		}
	}
    ```

    > A resolver takes care of giving you an instance of a type that you request. This specific one wraps Autofac and exposes it as a generic resolver. Good and handy if you decide to replace Autofac later on in the project.

1. Open App.xaml.cs in the LeetPhotos project and paste the code below into the constructor after InitializeComponent(). If there are other code you can remove it.

    ```csharp
    var navigationHelper = new FormsNavigationHelper(this);
    navigationHelper.RegisterViewsInAssembly(Assembly.GetExecutingAssembly());

    var builder = new ContainerBuilder();

    builder.RegisterInstance<INavigationHelper>(navigationHelper);

    // ViewModels
    var coreAssembly = Assembly.Load(new AssemblyName("LeetPhotos.Core"));
    builder.RegisterAssemblyTypes(coreAssembly)
            .Where(x => x.Name.EndsWith("ViewModel", StringComparison.Ordinal));

    builder.RegisterType<RestClient>().As<IRestClient>();
    builder.RegisterType<PhotoService>().As<IPhotoService>();

    var container = builder.Build();
    Resolver.SetResolver(new AutofacResolver(container));

    ViewModel.BeginInvokeOnMainThread = (action) => Device.BeginInvokeOnMainThread(action);
    ```

    > First we initialize the navigation helper by registering all the available views into TinyNavigationHelper. We can then navigate to views based on their class name.
    >
    > After that we create a ContainerBuilder. This is always done at startup. You can also pass the builder to platform specific code to register platform specific implementations of interfaces.
    >
    >Then we register all classes that ends with ```ViewModel``` from the core project.
    >
    >We move on to registering services such as ```RestClient``` and ```PhotoService```. We also provide what interface we want to be associated so when we ask for a ```IRestClient```, we will receive an instance of ```RestClient```.
    >
    >When all registration is complete we build the container and register it to the resolver. The resolver (that we added earlier) will then be our entrypoint for locating actual objects that implement interfaces that we ask for.
    >
    >Last but not least we provide the core project with an entrypoint to the main thread so that we can execute code on the UI thread from the UI agnostic core library.

1. Resolve references for the code in App.xaml.cs.
1. Create a new Forms ContentPage XAML in the Views folder and name it PhotosView.xaml.
1. Go to App.xaml.cs and add set the new Page/View as MainPage.
    ```csharp
    NavigationHelper.SetRootView(nameof(PhotosView));
    ```

    > The NavigationHelper uses the name of the view we want to set as a rootview and creates it internally. This is a feature of TinyNavigationHelper, not Xamarin.Forms.

1. In the constructor of PhotosView.xaml.cs and Set BindingContext to the PhotosViewModel.
    ```csharp
    BindingContext = Resolver.Resolve<PhotosViewModel>();
    ```

    > The BindingContext is what makes Xamarin.Forms an excellent candidate for MVVM. This means that we will have access to whatever object you assign to it from the View itself (Through a ```{Binding Property}``` statement.)
1. Go to PhotosView.xaml and set title on ContentPage to Leet Photos.
    ```xml
    <ContentPage Title="Leet Photos">
    ```
1. Add a ListView to the PhotosView and bind ItemsSource to the Photos property. Set the RowHeight to 300.
    ```xml
    <ListView ItemsSource="{Binding Photos" RowHeight="300">
    </ListView>
    ```
1. In the ListView, add a template for each item.
    ```xml
    <ListView ItemsSource="{Binding Photos}">
        <ListView.ItemTemplate>
            <DataTemplate>
            </DataTemplate>
        </ListView.ItemTemplate>
    </ListView>
    ```

    > The object bound to the ItemSource will be the ViewModels Photos property which is a list of photos. Each row will then set the BindingContext to each item in that list as it's being rendered.

1. In a ListView, every row needs to be a Cell, so you need to add all content in a ViewCell.
    ```xml
    <ListView ItemsSource="{Binding Photos">
        <ListView.ItemTemplate>
            <DataTemplate>
            <ViewCell>
            </ViewCell>
            </DataTemplate>
        </ListView.ItemTemplate>
    </ListView>
    ```
1. In the ViewCell create a Grid with two rows and a bottom margin of 5. The Image should have a RowSpan of two so the content in the second row will be an overlay.
    ```xml    
    <Grid Margin="0,0,0,5">
        <Grid.RowDefinitions>
            <RowDefinition Height="*" />
            <RowDefinition Height="60" />
        </Grid.RowDefinitions>
        
        <Image Grid.RowSpan="2" Source="{Binding Item.PhotoUrl}" Aspect="AspectFill" HorizontalOptions="Fill" VerticalOptions="Fill" />
        <BoxView Grid.Row="1" BackgroundColor="Black" Opacity="0.4" />
        <StackLayout Grid.Row="1" Padding="10,5">
            <Label Text="{Binding Item.Author}" TextColor="White" FontSize="Large" />
            <Label Text="{Binding Item.DateTaken}" TextColor="White" FontSize="Small" />
        </StackLayout>
    </Grid>
    ```

1. Remove the default row separator of the ListView by setting SeperatorVisibility to None.
    ```xml
        <ListView SeparatorVisibility="None" ...>
    ```
1. To make the ListView reuse cells that not are visible om the screen, add CachingStrategy, RecycleElement to the ListView.
    ```xml
        <ListView CachingStrategy="RecycleElement" ...>
    ```
1. Run the app! You will hopefully se photos of awesome Ninjas!
1. Next step is to handle when you're toucing an image. The best way to do that is to bind to the SelectedItem property to a property in the ViewModel.
    ```xml
        <ListView SelectedItem="{Binding SelectedPhoto}" ...>
1. If you don't unselect the ListView you will not be able to navigate to the same item again without navigating to another photo first. The first you need to to is to give the List a name.
     ```xml
    <ListView x:Name="PhotoList" ...>
    ```
1. After that go the code behind (PhotosView.xaml.cs, not the podcast) and subscribe to the ItemSelected event in an override of OnAppearing.
    ```csharp
    protected override void OnAppearing()
	{
		base.OnAppearing();

		PhotoList.ItemSelected += Photo_Selected;
	}    

    private void Photo_Selected(object sender, EventArgs e)
	{
		if(PhotoList.SelectedItem != null)
		{
			PhotoList.SelectedItem = null;
		}
	}
    ```
    It is also important to unsubscribe from the event when the user leaving the page/view.
    ```csharp
    protected override void OnDisappearing()
	{
		base.OnDisappearing();

		PhotoList.ItemSelected -= Photo_Selected;
	}
    ```

    > The Code Behind is a podcast by Daniel Hindrikes and Johan Karlsson about development, often with a .NET and mobile focus. There are an episode about Xamarin.Forms, tips and trix. Read more on https://thecodebehind.net.

1. Create a new Forms ContentPage XAML in the Views folder and name it PhotoDetailView.xaml.
1. Go to PhotoDetailView.xaml and set title on ContentPage to the title of the Photo.
    ```xml
    <ContentPage Title="{Binding Title}" ...>
    ```
1. Add a ScrollView to the Page so the user can scroll if the content is heigher than the screen height.
    ```xml
    <ContentPage ...>
    <ScrollView>
    </ScrollView>
    </ContentPage>
    ```
1. Create a Grid with two rows, the first for the Image, 300 in height and one for the details.
    ```xml
    <Grid>
        <Grid.RowDefinitions>
            <RowDefinition Height="300" />
            <RowDefinition Height="*" />
        </Grid.RowDefinitions>
        <Image Source="{Binding Url}" Aspect="AspectFill" HeightRequest="300" />
    </Grid>
    ```
1. As row one add a StackLayout with padding.
    ```xml
    <StackLayout Padding="10" Grid.Row="1">

    </StackLayout>
    ```
1. In the StackLayout, add the title and labels for photo details. Use font size medium for the headsers and micro for the rest of the labels. Use bold labels for headers.

    ```xml
     <Label Text="{Binding Title}" FontAttributes="Bold" FontSize="Medium" />

    <Label Text="Taken by" FontAttributes="Bold" FontSize="Micro" />
    <Label Text="{Binding Author}" FontSize="Micro" />
    
    <Label Text="Photo taken" FontAttributes="Bold" FontSize="Micro" />
    <Label Text="{Binding DateTaken}" FontSize="Micro" />
    
    <Label Text="Tags" FontAttributes="Bold" FontSize="Micro" />
    <Label Text="{Binding Tags}" FontSize="Micro" />
    ```
1. Run the app :D

## Exercise 2 - Pull to refresh
1. First you need to enable pull to refresh on the ListView.
    ```xml
    <ListView IsPullToRefreshEnabled="true" ...>
    ```
2. If you want to show a loading indicator when reloading you can bind a bool property in the ViewModel to IsRefreashing. If you like in this case binding to IsBusy it will show the loading indicator even on the initialize load of the feed. If you doesn't want that behavior you can create another property in the ViewModel.
    ```xml
    <ListView IsRefreshing="{Binding IsBusy}" IsPullToRefreshEnabled="true"  ...>
    ```
3. Last step to enable pull to refresh is to specify which Command that should run when the user is triggering a refresh. To do that bind the refresh command in the ViewModel to the RefreshCommand property of the ListView.
    ```xml
    <ListView IsPullToRefreshEnabled="true" IsRefreshing="{Binding IsBusy}" RefreshCommand="{Binding Refresh}" ...>
```
