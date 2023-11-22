package de.dmitrij.patuk.insurance.backend.api.configuration;

import de.dmitrij.patuk.insurance.backend.api.mapper.UserProfileEntityToDtoMapper;
import org.springframework.context.annotation.Bean;
import org.springframework.context.annotation.ComponentScan;
import org.springframework.context.annotation.Configuration;

@Configuration
@ComponentScan("de.dmitrij.patuk.insurance.backend")
public class Config {

    @Bean
    public UserProfileEntityToDtoMapper createUserProfileEntityToDtoMapper(){
        return new UserProfileEntityToDtoMapper();
    }
}
