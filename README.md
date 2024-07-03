# Application Web de Gestion des Congés

## Description

L'application web de gestion des congés est conçue pour faciliter la gestion des demandes de congés au sein de notre entreprise. 
Elle permet aux employés de soumettre des demandes de congés, aux gestionnaires de les approuver ou de les rejeter, 
et de gérer les soldes de congés. L'application fournit également des fonctionnalités de reporting pour suivre l'utilisation des congés.

## Objectifs

- Faciliter la soumission et l'approbation des demandes de congés.
- Automatiser la gestion des soldes de congés.
- Offrir des fonctionnalités de reporting pour un suivi efficace des congés.

## Technologies

- **Backend** : .NET Core
- **Frontend** : Angular
- **Base de données** : SQL Server

## Fonctionnalités

### 1. Authentification et Autorisation

- Authentification des utilisateurs pour accéder à l'application.
- Trois rôles d'utilisateur : employé, gestionnaire et administrateur, chacun avec des autorisations spécifiques.

### 2. Demande de Congé

- Soumission de demandes de congé par les employés avec date de début, date de fin, type de congé et justification.
- Notification des employés sur le statut de leur demande (en attente, approuvée, rejetée).
- Notifications aux gestionnaires pour examiner et approuver/rejeter les demandes de congé.

### 3. Gestion des Soldes de Congés

- Mise à jour automatique des soldes de congés (congés payés, non payés, etc.) en fonction des demandes approuvées ou rejetées.
- Consultation des soldes de congés actuels par les employés.

### 4. Calendrier des Congés

- Affichage d'un calendrier montrant les congés planifiés pour l'ensemble de l'entreprise.
- Visualisation des congés approuvés et des jours de congé disponibles pour chaque employé.

### 5. Reporting

- Accès à des rapports sur l'utilisation des congés pour les gestionnaires et les administrateurs.
- Rapports incluant les demandes en attente, approuvées, rejetées et les soldes de congés actuels.

### 6. Profil Utilisateur

- Profil personnel pour chaque utilisateur où il peut mettre à jour ses informations personnelles (nom, adresse, numéro de téléphone, etc.).

### 7. Sécurité

- Protection contre les menaces courantes telles que l'injection SQL, CSRF et les attaques par force brute.
- Protection des données personnelles des employés conformément aux réglementations sur la confidentialité des données.

### 8. Interface Utilisateur

- Interface utilisateur conviviale et réactive.
- Respect des meilleures pratiques en matière d'expérience utilisateur.

### Lien vers le front-end de l'application de gestion de congé
lien: https://github.com/Timothe00/LeaveAppManagementFrontEnd

## Licence

Ce projet est sous licence MIT. Voir le fichier [LICENSE](LICENSE) pour plus de détails.

## Contact

Pour toute question ou suggestion, veuillez contacter : 
- Email : yaofrancistimothee@gmail.com
