package de.dmitrij.patuk.insurance.backend.data.repositories;

import de.dmitrij.patuk.insurance.backend.data.entities.UserProfileEntity;
import org.springframework.data.repository.ListCrudRepository;
import org.springframework.stereotype.Repository;

@Repository
public interface UserProfileRepository extends ListCrudRepository<UserProfileEntity, String> {
}
