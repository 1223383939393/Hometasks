import java.util.Objects;
public class BankAccount {
    String numOfAcc;
    String owner;
    double balance;
    String typeOfBankAcc;

    BankAccount(String numOfAcc, String owner, double balance, String typeOfBankAcc) {
        this.numOfAcc = numOfAcc;
        this.owner = owner;
        this.balance = balance;
        this.typeOfBankAcc = typeOfBankAcc;
    }
    BankAccount(String numOfAcc, String owner, double balance) {
        this.numOfAcc = numOfAcc;
        this.owner = owner;
        this.balance = balance;
        typeOfBankAcc = "CHECKING";
    }

    String getNumOfAcc() {
        return numOfAcc;
    }
    String getName() {
        return owner;
    }
    double getBalance() {
        return balance;
    }
    String getTypeOfBankAcc() {
        return typeOfBankAcc;
    }

    public void setName(String owner) {
        if (owner == null || owner.isBlank()) {
            System.out.println("Имя не может быть пустым");
            return;
        }
        this.owner = owner;
    }
    public void deposit(double amount) {
        if (amount <= 0) {
            System.out.println("Ошибка");
            return;
        }
        else {
            balance += amount;
        }
    }
    public void withdraw(double amount) {
        if (amount <= 0) {
            System.out.println("Ошибка");
            return;
        }
        else if (amount > getBalance()) {
            System.out.println("Недостаточно средств");
            return;
        }
        else {
            balance -= amount;
        }
    }
    public void transfer(BankAccount target, double amount) {
        if (target != null && target != this) {
            this.withdraw(amount);
            target.deposit(amount);
        }
        else {
            System.out.println("Ошибка");
            return;
        }
    }
    public boolean isPositive() {
        if (getBalance() < 0 && getTypeOfBankAcc().equals("CREDIT")) {
            return true;
        } else if (getBalance() < 0) {
            return false;
        }
        else {
            return true;
        }
    }
    public void getInfo() {
        System.out.println("Название - " + getNumOfAcc() + " (" + getName() + ", " + getTypeOfBankAcc() + "), " + getBalance());
    }
    @Override
    public boolean equals(Object obj) {
        if (this == obj) return true;
        if (obj == null || getClass() != obj.getClass()) return false;

        BankAccount other = (BankAccount) obj;

        return numOfAcc.equals(other.numOfAcc);
    }

    @Override
    public int hashCode() {
        return Objects.hash(numOfAcc);
    }
}
