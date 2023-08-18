# Public-Affairs-Portal
![image](https://github.com/Jamshidnet/Public-Affairs-Portal/assets/122851582/a9926526-7ab9-48ea-9c09-1d61ba9142a6)

I have developed a .NET project named "Public Affairs Portal," equipped with a range of compelling features. This platform enables registered users to conduct interviews with citizens based on a database of predefined questions. These questions come with both default answers and options for user-generated responses. The project encompasses various entities, including users, questions, answers, and documents.

One of the notable features is the document entity, which involves attributes like the creator user, client questions, and selected/entered answers for the document. I've also implemented diverse filters for documents, allowing users to extract statistical insights in formats such as Excel and PDF. For instance, you can effortlessly generate counts of documents categorized by question types, segmented by region, district, or quarter.

To ensure robust authentication, I've integrated permission-based JWT (JSON Web Tokens) mechanisms. This approach guarantees that each action within the controllers is permission-bound and is centrally managed through the database. Additionally, I've established a comprehensive system for tracking user actions and changes made to documents, fostering transparency and accountability. Lastly, I used Fluent Validation for validation purposes. 

For the database architecture, I've adopted a "database first" approach using EF Core, with entity models designed for PostgreSQL. The entire project includes the necessary scripts for creating these models in the database. To efficiently handle requests, I've incorporated MediatR, enhancing the project's responsiveness and scalability.

In summary, the "Public Affairs Portal" showcases a robust array of features, facilitating citizen interviews, comprehensive document management, insightful data analytics, and secure user interactions. The use of permission-based JWT, advanced logging, and MediatR ensures a secure and efficient user experience. The project's entity models, database scripts, and application logic together form a cohesive and versatile solution.

https://dbdiagram.io/d/64df610702bd1c4a5e01a529



