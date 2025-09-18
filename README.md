
# University Academic & Student Management on Blockchain (FAP Blockchain)

## Overview
FAP Blockchain is a comprehensive system for managing university academic records, including student grades, attendance, class enrollments, and credential verification, powered by Ethereum Quorum (permissioned blockchain). It ensures tamper-proof, transparent, and scalable management of credentials and academic records, providing secure and real-time access for students, teachers, and employers.

## Features
- **Students**: Access academic records, grades, and credentials securely via blockchain, shareable with employers via QR code or link.
- **Teachers**: Manage classes, mark attendance, and update student grades securely on-chain.
- **Employers/Third Parties**: Verify student credentials and grades in real-time.
- **Admin**: Manage users (students, teachers), issue and revoke credentials, and generate reports.

## Architecture
The system is built on a hybrid architecture combining on-chain (Ethereum Quorum) and off-chain (ASP.NET Core backend) components.

### On-Chain (Blockchain Layer)
- **Smart Contracts (Solidity on Ethereum Quorum)**: Used for managing credentials, attendance, and grades.
- **Role-Based Access Control (RBAC)**: Admin, Teacher, and Student roles for different levels of access.

### Off-Chain (Hybrid Architecture)
- **Backend API (ASP.NET Core)**: Acts as a bridge between the frontend and blockchain.
- **SQL Server Database**: Stores non-sensitive metadata like user profiles, class details, attendance logs, and blockchain event data.
- **Event Listener Service**: Synchronizes off-chain data with blockchain events (e.g., credential issuance, grade updates).

### Frontend
- **React (TypeScript)** + **Ethers.js** + **MetaMask/WalletConnect**: Used for building the frontend, providing an intuitive dashboard for users.
- **Admin Portal**: Manage students, teachers, and credentials.
- **Teacher Portal**: Assign students to classes, mark attendance, and update grades.
- **Student Portal**: View academic records and generate verifiable links or QR codes for sharing credentials.
- **Public Verification Portal**: Employers can verify student credentials using the provided QR codes or links.

## Tech Stack
- **Backend**: ASP.NET Core, Entity Framework Core, SQL Server
- **Blockchain**: Ethereum Quorum, Solidity (Smart Contracts)
- **Frontend**: React, TypeScript, Ethers.js, MetaMask/WalletConnect
- **Database**: SQL Server (for off-chain metadata)
- **Hosting**: Docker (for deployment)

## Installation

### Prerequisites
- .NET SDK 8.x or higher
- SQL Server or Docker (for local development)
- Node.js and npm for frontend setup

### Backend Setup
1. Clone the repository:
   ```bash
   git clone https://github.com/your-username/FapBlockchain.git
   cd FapBlockchain/Fap.Api
````

2. Install dependencies:

   ```bash
   dotnet restore
   ```

3. Set up the database:

   * Configure the connection string in `appsettings.json`.
   * Run migrations to set up the database:

     ```bash
     dotnet ef database update
     ```

4. Run the backend:

   ```bash
   dotnet run
   ```

### Frontend Setup

1. Navigate to the `FapFrontend` directory:

   ```bash
   cd FapBlockchain/FapFrontend
   ```

2. Install frontend dependencies:

   ```bash
   npm install
   ```

3. Run the frontend:

   ```bash
   npm start
   ```

## Usage

1. Access the **API** on `http://localhost:5160` or `https://localhost:7025`.
2. Access the **Swagger UI** at `http://localhost:5160/swagger`.
3. Use the **Admin**, **Teacher**, or **Student** portals for testing and interacting with the system.

## License

This project is licensed under the MIT License - see the [LICENSE](LICENSE) file for details.

## Contributing

1. Fork the repository.
2. Create your feature branch (`git checkout -b feature-name`).
3. Commit your changes (`git commit -m 'Add new feature'`).
4. Push to the branch (`git push origin feature-name`).
5. Create a new pull request.

## Acknowledgments

* Inspired by the need for secure and verifiable academic record management.
* Special thanks to the developers of **Ethereum Quorum** and **Solidity** for providing the blockchain framework.



