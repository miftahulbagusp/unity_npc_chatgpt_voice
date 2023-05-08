# Simple NPC use chatGPT & voice command

Simple implementation of NPC interaction using ChatGPT and Google Cloud Speech to Text

## How To Use

### Importing the OpenAI Unity Package
Follow these steps:
- Go to `Window > Package Manager`
- Click the `+` button and select `Add package from git URL`
- Paste the repository URL https://github.com/srcnalt/OpenAI-Unity.git and click `Add`

### Setting Up Your OpenAI Account
To use the OpenAI API, you need to have an OpenAI account. Follow these steps to create an account and generate an API key:

- Go to https://openai.com/api and sign up for an account
- Once you have created an account, go to https://beta.openai.com/account/api-keys
- Create a new secret key and save it

### Saving Your Credentials
To make requests to the OpenAI API, you need to use your API key and organization name (if applicable). To avoid exposing your API key in your Unity project, you can save it in your device's local storage.

You can pass your API key & organization into `API_KEY` & `ORGANIZATION` constant static variable in `ChatGPT` script,but this is not recommended!
```csharp
const string API_KEY = "sk-Me8...6yi";
const string ORGANIZATION = "org-ij...GOy";
```

### Build and run in Android & iOS

