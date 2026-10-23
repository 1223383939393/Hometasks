import java.time.LocalDate;
public class Library {
    String name;
    int year;
    Book[] bookShelf = new Book[20];
    int count = 0;

    Library(String name, int year) {
        this.year = year;
        this.name = name;
    }
    Library(String name) {
        this.name = name;
        year = LocalDate.now().getYear();
    }
    public void addBook(Book b) {
        if (b == null) {
            System.out.println("Пустая книга");
            return;
        } else if (count == 20) {
            System.out.println("Переполнено");
            return;
        }
        for (int i = 0; i < count; i++) {
            if (bookShelf[i].equals(b)) {
                System.out.println("Дубликат");
                return;
            }
        }
        System.out.println("ecg");
        bookShelf[count] = b;
        count++;
    }
}
