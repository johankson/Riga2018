# Xamarin.Forms Workshop - Riga 2018
During this workshop we will create a photo app that fetches photos from Flickr that are tagged with tretton37.

To build the app we will use Xamarin.Forms. 

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
1. Click Create.
1. Select either the iOS- or Android project as startup and run the app.
1. Gratz! You have created your first native cross platform app.
1. Add references to LeetPhotos.Core on both iOS- and Android project.
1. Install Autofac, Newtonsoft.Json and TinyNavigationHelper.Forms on both iOS and Android project.
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
1. Resolve references for the code in App.xaml.cs.
1. Create a new Forms ContentPage in the View and name it PhotosView.xaml.
1. Go to App.xaml.cs and add set the new Page/View as MainPage.
    ```csharp
    NavigationHelper.SetRootView(nameof(PhotosView));
    ```
1. In the constructor of PhotosView.xaml.cs and Set BindingContext to the PhotosViewModel.
    ```csharp
    BindingContext = Resolver.Resolve<PhotosViewModel>();
    ```
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
    <ListView ItemsSource="{Binding Photos">
        <ListView.ItemTemplate>
            <DataTemplate>
            </DataTemplate>
        </ListView.ItemTemplate>
    </ListView>
    ```
1. I a ListView, every row needs to be a Cell, so you need to add all content in a ViewCell.
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
1. Run the app!