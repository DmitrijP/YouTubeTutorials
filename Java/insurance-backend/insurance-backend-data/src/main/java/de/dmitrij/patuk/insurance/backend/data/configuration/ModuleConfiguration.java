package de.dmitrij.patuk.insurance.backend.data.configuration;

import org.springframework.boot.autoconfigure.domain.EntityScan;
import org.springframework.context.annotation.Configuration;
import org.springframework.data.jpa.repository.config.EnableJpaRepositories;

@Configuration
@EntityScan({
        "de.dmitrij.patuk.insurance.backend.data.entities"
})
@EnableJpaRepositories("de.dmitrij.patuk.insurance.backend.data.repositories")
public class ModuleConfiguration {
}
