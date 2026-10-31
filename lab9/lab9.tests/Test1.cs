using Microsoft.VisualStudio.TestTools.UnitTesting;
using Lab9;

namespace Lab9.Tests
{
    [TestClass]
    public class MoneyTests
    {
        [TestMethod]
        public void SubtractDoesNotGoBelowZero()
        {
            // Если вычитаем больше, чем есть, результат должен обнулиться
            Money first = new Money(10, 0);
            Money second = new Money(20, 0);

            Money result = first.Subtract(second);

            Assert.AreEqual(0, result.Rubles);
            Assert.AreEqual(0, result.Kopeks);
        }

        [TestMethod]
        public void IncrementMovesKopeksToRubles()
        {
            // 5.99 + 1 копейка должно стать 6.00, а не 5.100
            Money money = new Money(5, 99);

            money++;

            Assert.AreEqual(6, money.Rubles);
            Assert.AreEqual(0, money.Kopeks);
        }

        [TestMethod]
        public void IndexerThrowsWhenOutOfRange()
        {
            // Индексатор должен кидать ArgumentException при выходе за границы
            MoneyArray wallets = new MoneyArray(3);

            Assert.ThrowsException<ArgumentException>(() => wallets[5]);
        }

        [TestMethod]
        public void MaxReturnsLargestWallet()
        {
            // Сравниваем по сумме копеек: 10.01 больше, чем 10.00 и 5.99
            MoneyArray wallets = new MoneyArray(3);
            wallets[0] = new Money(10, 0);
            wallets[1] = new Money(5, 99);
            wallets[2] = new Money(10, 1);

            Money max = wallets.GetMax();

            Assert.AreEqual(10, max.Rubles);
            Assert.AreEqual(1, max.Kopeks);
        }
    }
}
