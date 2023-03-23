
# BookManager
## Activity developed in group for projeto interação 5by5
<div>
  <img src="https://raw.githubusercontent.com/devicons/devicon/1119b9f84c0290e0f0b38982099a2bd027a48bf1/icons/csharp/csharp-plain.svg" title="CSharp" alt="Csharp" width="40" height="40"/>&nbsp;<img src="https://raw.githubusercontent.com/devicons/devicon/1119b9f84c0290e0f0b38982099a2bd027a48bf1/icons/visualstudio/visualstudio-plain.svg" title="VisualStudio" alt="VisualStudio" width="40" height="40"/>&nbsp;<img src="https://github.com/devicons/devicon/blob/master/icons/git/git-original-wordmark.svg" title="Git" **alt="Git" width="40" height="40"/>
</div>

Program that simulates a bookshelf where you can store, remove and list your books. The information will be stored in a file in the user’s directory.

## Requirements

Battleship requires .NET 6.0 to run. [Install here](https://dotnet.microsoft.com/en-us/download/dotnet/6.0)

To install the dependencies use the command below.

```sh
dotnet restore
```

Use the commands below to open and run the application.

```sh
dotnet build
dotnet run
```

## Guide

There are 5 options: Add, remove, list, change status and sort the bookshelf

You can choose one of the functionalities from the program menu.

![Menu](https://user-images.githubusercontent.com/89887370/227203604-73d9aa6d-0737-477c-89fc-e4b98ad6f3e5.PNG)


### Add

Enter information such as book title, publisher and authors into the program and it will automatically generate a fictitious ISBN and store it in the bookshelf.

### Remove

All books stored in the created file will be displayed. If there are none, it will be notified that it is empty.

Choose an item from the list to be removed.

### List

All books stored in the created file will be displayed. If there are none, it will be notified that it is empty.

### Change status

All books stored in the created file will be displayed. If there are none, it will be notified that it is empty.

Choose an item from the list to have the status changed.

### Sort

You can sort your list by name, your writers, or by current status.

## Development

Development logic flowchart

<img src="https://user-images.githubusercontent.com/89887370/227203893-0d87e9b2-3b31-4878-b51a-8d34ff2c3ae2.png" title="Flowchart" alt="Diagrama de lógica da criação da estante digital" width="500"/>
