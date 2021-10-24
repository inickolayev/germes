using System;

namespace Germes.Accountant.Implementations.Translations
{
    public static class AccountantText
    {
        public static string WelcomeUser(string userName)
            => string.IsNullOrEmpty(userName)
                ? $"Добро пожаловать, {userName}!"
                : "Добро пожаловать!";

        public static string AddedNewCategory(string categoryName)
            => $"Добавлена новая категория расходов \"{categoryName}\"";

        public static string AddedNewExpenseTransaction(string categoryName, DateTime @from, DateTime to,
            decimal categoryBalance)
        {
            var fromStr = from.ToShortDateString();
            var toStr = to.ToShortDateString();
            return $"Траты по категории \"{categoryName}\" ({fromStr} - {toStr}): {categoryBalance:0} руб.";
        }
        
        public static string AddedNewIncomeTransaction(string categoryName, DateTime @from, DateTime to,
            decimal categoryBalance)
        {
            var fromStr = from.ToShortDateString();
            var toStr = to.ToShortDateString();
            return $"Получения по категории \"{categoryName}\" ({fromStr} - {toStr}): {categoryBalance:0} руб.";
        }

        public static string RemainingBalance(decimal balance)
            => $"Остаток по счету: {balance:0} руб.";

        public const string HelpTitle = "Плагин \"Счетовод\"";
        
        public const string AddTransactionDescription = "добавить новый расход/приход";
        public const string GetBalanceDescription = "получить баланс";
        public const string NoTransactionsFound = "У вас нет ни одной записи...";
    }
}