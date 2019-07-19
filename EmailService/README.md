# How to use:  

```c#
private readonly IEmailService _emailService;
```
```c#
//lorem ipsum example ;)
string plainText = @"Emma,

    Quisque sit amet ultricies odio. Vivamus volutpat blandit eros ut dictum. 
    Aenean finibus nec mauris at auctor. 
    Integer eleifend neque a nulla consectetur consectetur.

    Quisque tempus finibus justo?
    
    Aliquam erat volutpat
    John <3
    "
```
```c#
private void ExampleSendPlain()
{
    _emailService.Message()
        .From("John", "john@email.com")
        .To("Emma", "emma@email.com")
        .WithSubject("Email subject goes here")
        .WithBodyPlain(plainText)
        .Send();
}
```

# Dependency Injection

1. Create section in appsettings.json

javascript
```javascript
"SmtpClient": {
    "Address": "",
    "Port": "",
    "Username": "",
    "Password": "",
    "UseSsl": ""
  }
```
E.g. 
```javascript
"SmtpClient": {
    "Address": "",
    "Port": "",
    "Username": "",
    "Password": "",
    "UseSsl": "true"
  }
```

## Library based on  

[MailKit - Open Source cross-platform .NET mail-client library that is based on MimeKit and optimized for mobile devices](https://www.nuget.org/packages/MailKit)