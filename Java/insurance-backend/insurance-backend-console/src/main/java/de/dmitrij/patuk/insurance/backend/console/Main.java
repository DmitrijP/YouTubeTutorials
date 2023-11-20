package de.dmitrij.patuk.insurance.backend.console;


import de.dmitrij.patuk.insurance.backend.data.entities.UserProfileEntity;
import de.dmitrij.patuk.insurance.backend.data.repositories.UserProfileRepository;
import org.springframework.beans.factory.annotation.Autowired;
import org.springframework.boot.CommandLineRunner;
import org.springframework.boot.SpringApplication;
import org.springframework.boot.autoconfigure.SpringBootApplication;
import org.springframework.context.annotation.ComponentScan;

@SpringBootApplication
@ComponentScan("de.dmitrij.patuk.insurance.backend")
public class Main  implements CommandLineRunner {
    public static void main(String[] args) {
        SpringApplication.run(Main.class, args);
    }

    @Autowired
    private UserProfileRepository repository;

    @Override
    public void run(String... args) throws Exception {
        var user = new UserProfileEntity();
        user.setUserName("dmitrijP");
        user.setFirstName("Dmitrij");
        user.setLastName("Patuk");
        var savedUser = repository.save(user);
        System.out.println("Created User + " + savedUser.getId());

        var user1 = new UserProfileEntity();
        user1.setUserName("dmitrijP 1");
        user1.setFirstName("Dmitrij 1");
        user1.setLastName("Patuk 1");
        var savedUser1 = repository.save(user1);
        System.out.println("Created User + " + savedUser1.getId());

        var user2 = new UserProfileEntity();
        user2.setUserName("dmitrijP 2");
        user2.setFirstName("Dmitrij 2");
        user2.setLastName("Patuk ");
        var savedUser2 = repository.save(user2);
        System.out.println("Created User + " + savedUser2.getId());

        var foundUserOpt = repository.findById(savedUser1.getId());
        if(foundUserOpt.isPresent()){
            var foundUser = foundUserOpt.get();
            System.out.println("Found User + " + foundUser.getId());
        }

        System.out.println("Hallo");
    }
}