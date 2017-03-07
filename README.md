<img src="https://upload.wikimedia.org/wikipedia/commons/thumb/c/cf/Tudor_Rose_Royal_Badge_of_England.svg/2000px-Tudor_Rose_Royal_Badge_of_England.svg.png" width="100" height="150" /> 
# The Gilded Rose Project
### I - Introduction
The Gilded Rose solution is a .NET 4.5 Web API 2 project developed using C# to help Allison, a shopkeeper to expand her store by offering to her merchants in other cities access to her shop's inventory via HTTP. 

The API exposes 2 HTTP functions:
```csharp
Inventory GetInventory();
Status BuyItem(Transaction transaction);
```
### II - Prerequisities:
##### Debugging
You need to have a Visual Studio version that supports .NET Framework 4.5.
```
Example: Microsoft Visual Studio Community 2015 with .NET Framework 4.5
```
##### Hosting
```
Microsoft Windows OS with IIS 7.5 or greater
```
###### Note: 
To maximize security, it is recommended to host the API under a site with an HTTPS binding only. SSL/TLS connection with a certificate signed by a CA(Certificate Authority) provides a safe mechanism to protect the user data such as "AccessToken" which will be transmitted over the internet in order to successfully make a purchase.

### III - Design
##### 1. GetInventory
###### Description: 
This function takes in no input parameters and returns an inventory object. The inventory object contains a **Status** object and an **IList<Item>**. For more info on these objects, consult the **Objects** section below. This function does not require a user to be authenicated and is accessible using ``` HTTP GET ``` at the following sample url: [https://server_name:port/api/inventory/getinventory](https://server_name:port/api/inventory/getinventory).
###### Sample Request (AngularJS - No input parameters required for this function.)
```angularjs
$http.get('/api/inventory/getinventory').then(successCallback, errorCallback);
```

###### Sample Response:
```json
{"Status":{"code":0,"message":"Successful"},"Items":[{"ID":1,"Name":"MacBook Air","Description":"Whatever the task, new fifth-generation Intel Core i5 and i7 processors with Intel HD Graphics 6000 are up to it.","Price":1099,"Quantity":10},{"ID":2,"Name":"Burton Custom Mystery Snowboard","Description":"The absolute lightest weight science in the entire Burton line applied to the Custom’s versatile and venerated shape. Simply genius.","Price":500,"Quantity":0},{"ID":3,"Name":"GoPro Hero 4","Description":"HERO4 Session is the most wearable and mountable GoPro ever.","Price":559,"Quantity":100}]}
```
Note: The API can respond in a JSON or XML format.


##### 2. BuyItem
###### Description: 
This function takes in a **Transaction** input parameter and returns a **Status** object which indicates if the purchase was successful or not. The **Transaction** object is composed of two objects. A **User** object and an **Item** object. The merchant needs to be authenicated first, i.e. the merchant needs to acquire an API Access Token before being allowed to call this function. This function has to be accessed by using an ``` HTTP POST ``` method at the following sample url: [https://server_name:port/api/inventory/buyitem](https://server_name:port/api/inventory/buyitem).

* Data Format: JSON
* Authenication mechanism: Token based authenication (Hash produced using SHA256). 
    * Stateless, no variables are stored on the server, which makes it scalable.
    * Added security with Token expiry date, plus token can be expired anytime.
    * Scalable, it allows us to have multiple servers available to serve requests since token can be computed / a user can be authenicated on any of the servers.
    * Mobile ready
    * Safe against CSRF (Cross-site request forgery)
###### Sample Request (AngularJS)
```angularjs
var data = {};
data.User.Username = "kennykolstad";
data.User.APIAccessToken = "af3ee1ad42ef87c8d9939b63db895283c900ab9bb46037060014a0b457bcee07";
data.Item.ID = 1;
data.Item.Quantity = 3;
$http.post('/api/inventory/buyitem', data).then(successCallback, errorCallback);
```
###### Sample Response:
```json
{"Status":{"code":0,"message":"Purchase Successful"}
```

### IV - Objects
##### 1. Status
```csharp
pubilc class Status{
    public int code {get;set;}
    public string message {get;set;}
}
```
##### 2. Item
```csharp
public class Item{
    public int ID { get; set; }
    public string Name { get; set; }
    public string Description { get; set; }
    public int Price { get; set; }
    public int Quantity { get; set; }
}
```
##### 3. Inventory
```csharp
public class Inventory
{
    public Status Status { get; set; }
    public IList<Item> Items { get; set; }
}
```
##### 4. User
```csharp
public class User{
    public string Username { get; set; }
    public string Password { get; set; }
    public string City { get; set; }

    public string APIAccessToken { get; set; }
    public string HashedPassword { get; set; }
    public DateTime TokenIssueDate { get; set; }
}
```
##### 5. Transaction
```csharp
[DataContract]
public class Transaction
{
    [DataMember()]
    public User User { get; set; }
    [DataMember()]
    public Item Item { get; set; }
}
```
### V - Testing
The Gilded Rose solution also contains a unit test project with multiple test cases. Please consult **TheGildedRose.Tests** project for more details on each method.

In addition to this, I included an html page, **index.html**, which has a sample ```AngularJS``` application that generates requests to the API and display the results. 

### VI - Usage
To open this project in Visual Studio follow the steps below:
* Open Visual Studio 2015 or later preferably (Make sure you have the github extension installed)
* Clone the project and launch the solution
* Build the solution
* Run the project and launch ***index.html*** or run ***TheGildedRose.Tests*** project to verify results.

***Tony***
