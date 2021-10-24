namespace Germes.Help.Implementations.Translations
{
    public static class WelcomeText
    {
        public static string WelcomeUser(string userName)
            => $@"
Приветствую, {userName}!
Меня зовут Гермес и я помогу разобраться с вашими финансами.
Если хотите узнать, что я могу - напишите /help или помощь
";
        
        public static string Help(string pluginDescriptions)
            => $@"
Текущие плагины:

{pluginDescriptions}

Если что-то не будет понятно - напишите /help или помощь.
";
    }
}