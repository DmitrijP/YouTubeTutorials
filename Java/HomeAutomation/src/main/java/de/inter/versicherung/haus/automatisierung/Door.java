package de.inter.versicherung.haus.automatisierung;

public class Door {
    private boolean isOpen;
    private final String name;

    public Door(String name) {
        this.name = name;
    }

    public Door() {
        name = "undefined";
    }

    public void toggleOpen() {
        isOpen = !isOpen;
    }

    public boolean getIsOpen(){
        return isOpen;
    }

    public String getName() {
        return name;
    }


    @Override
    public String toString() {
        return "Door{" +
                "isOpen=" + isOpen +
                ", name='" + name + '\'' +
                '}';
    }
}
