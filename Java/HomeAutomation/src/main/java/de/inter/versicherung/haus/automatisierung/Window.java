package de.inter.versicherung.haus.automatisierung;

public class Window {
    private boolean isOpen;
    private final String name;

    public Window(String name) {
        this.name = name;
    }

    public Window() {
        this.name = "undefined";
    }

    public String getName() {
        return name;
    }

    public void toggleOpen() {
        isOpen = !isOpen;
    }

    public boolean getIsOpen(){
        return isOpen;
    }

    @Override
    public String toString() {
        return "Window{" +
                "isOpen=" + isOpen +
                ", name='" + name + '\'' +
                '}';
    }
}
