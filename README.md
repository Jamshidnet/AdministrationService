# Public-Affairs-Portal
![image](https://github.com/Jamshidnet/Public-Affairs-Portal/assets/122851582/a9926526-7ab9-48ea-9c09-1d61ba9142a6)

I have developed a .NET project named "Public Affairs Portal," equipped with a range of compelling features. This platform enables registered users to conduct interviews with citizens based on a database of predefined questions. These questions come with both default answers and options for user-generated responses. The project encompasses various entities, including users, questions, answers, and documents.

One of the notable features is the document entity, which involves attributes like the creator user, client questions, and selected/entered answers for the document. I've also implemented diverse filters for documents, allowing users to extract statistical insights in formats such as Excel and PDF. For instance, you can effortlessly generate counts of documents categorized by question types, segmented by region, district, or quarter.

To ensure robust authentication, I've integrated permission-based JWT (JSON Web Tokens) mechanisms. This approach guarantees that each action within the controllers is permission-bound and is centrally managed through the database. Additionally, I've established a comprehensive system for tracking user actions and changes made to documents, fostering transparency and accountability. Lastly, I used Fluent Validation for validation purposes. 

For the database architecture, I've adopted a "database first" approach using EF Core, with entity models designed for PostgreSQL. The entire project includes the necessary scripts for creating these models in the database. To efficiently handle requests, I've incorporated MediatR, enhancing the project's responsiveness and scalability.

In summary, the "Public Affairs Portal" showcases a robust array of features, facilitating citizen interviews, comprehensive document management, insightful data analytics, and secure user interactions. The use of permission-based JWT, advanced logging, and MediatR ensures a secure and efficient user experience. The project's entity models, database scripts, and application logic together form a cohesive and versatile solution.


Through this link you will see ERD for database of the project. 
https://dbdiagram.io/d/64df610702bd1c4a5e01a529





Я разработал проект .NET под названием «Портал по связям с общественностью», оснащенный рядом интересных функций. Эта платформа позволяет зарегистрированным пользователям проводить интервью с гражданами на основе базы данных заранее заданных вопросов. На эти вопросы есть как ответы по умолчанию, так и варианты ответов, созданных пользователем. Проект охватывает различные объекты, включая пользователей, вопросы, ответы и документы.

Одной из примечательных особенностей является сущность документа, которая включает в себя такие атрибуты, как пользователь-создатель, вопросы клиента и выбранные/введенные ответы для документа. Я также реализовал разнообразные фильтры для документов, позволяющие пользователям извлекать статистическую информацию в таких форматах, как Excel и PDF. Например, вы можете легко генерировать количество документов, классифицированных по типам вопросов, сегментированных по регионам, районам или кварталам.

Чтобы обеспечить надежную аутентификацию, я интегрировал механизмы JWT на основе разрешений (JSON Web Tokens). Такой подход гарантирует, что каждое действие внутри контроллеров ограничено разрешениями и централизованно управляется через базу данных. Кроме того, я создал комплексную систему для отслеживания действий пользователей и изменений, внесенных в документы, что способствует прозрачности и подотчетности. Наконец, я использовал Fluent Validation для целей проверки.

Для архитектуры базы данных я применил подход «сначала база данных», используя EF Core, с моделями сущностей, разработанными для PostgreSQL. Весь проект включает в себя необходимые скрипты для создания этих моделей в базе данных. Для эффективной обработки запросов я включил MediatR, повысив скорость реагирования и масштабируемость проекта.

Подводя итог, можно сказать, что «Портал по связям с общественностью» демонстрирует широкий набор функций, облегчающих проведение интервью с гражданами, комплексное управление документами, глубокий анализ данных и безопасное взаимодействие с пользователями. Использование JWT на основе разрешений, расширенного ведения журнала и MediatR обеспечивает безопасность и эффективность работы пользователя. Модели сущностей проекта, сценарии базы данных и логика приложения вместе образуют единое и универсальное решение.

По этой ссылке вы увидите ERD для базы данных проекта. https://dbdiagram.io/d/64df610702bd1c4a5e01a529

