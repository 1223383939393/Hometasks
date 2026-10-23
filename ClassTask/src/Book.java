import java.util.Objects;
public class Book {
    String name;
    String author;
    int year;
    int numOfPages;

    Book(String Name, String Author, int Year, int NumOfPages) {
        setName(Name);
        setAuthor(Author);
        setYear(Year);
        this.numOfPages = NumOfPages;
    }
    Book(String Name, String Author) {
        setName(Name);
        setAuthor(Author);
        this.year = 2026;
        this.numOfPages = 0;
    }
    String getName() {
        return name;
    }
    String getAuthor() {
        return author;
    }
    int getYear() {
        return year;
    }
    int getNumOfPages() {
        return numOfPages;
    }
    public void setName(String name) {
        if (name == null || name.isBlank()) {
            System.out.println("Имя не может быть пустым");
            return;
        }
        this.name = name;
    }
    public void setAuthor(String author) {
        if (author == null || author.isBlank()) {
            System.out.println("Имя не может быть пустым");
            return;
        }
        this.author = author;
    }
    public void setYear(int year) {
        if (year < 1000 || year > 2026) {
            System.out.println("Некорректный возраст: " + year);
            return;
        }
        this.year = year;
    }
    public boolean isModern() {
        if (getYear() > 2000) {
            return true;
        }
        else {
            return false;
        }
    }
    public boolean isShort() {
        if (getNumOfPages() < 200) {
            return true;
        }
        else {
            return false;
        }
    }
    public void getInfo() {
        System.out.println("Название - " + getName() + " Автор " + getAuthor() + "(" + getYear() + "), " + getNumOfPages() + "стр.");
    }
    @Override
    public String toString() {
        return "Person{name='" + name + "', author=" + author + "}";
    }
    @Override
    public boolean equals(Object obj) {
        if (this == obj) return true;
        if (obj == null || getClass() != obj.getClass()) return false;

        Book person = (Book) obj;
        return author.equals(person.author) && name.equals(person.name);
    }
    @Override
    public int hashCode() {
        return Objects.hash(name, author);
    }
}
