﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using ServerLibrary.Data;

#nullable disable

namespace ServerLibrary.Data.Migrations
{
    [DbContext(typeof(AppDbContext))]
    partial class AppDbContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "8.0.10")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder);

            modelBuilder.Entity("BaseLibrary.Entities.Admins", b =>
                {
                    b.Property<int>("Admin_Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Admin_Id"));

                    b.Property<int>("User_Id")
                        .HasColumnType("int");

                    b.HasKey("Admin_Id");

                    b.HasIndex("User_Id")
                        .IsUnique();

                    b.ToTable("Admins");
                });

            modelBuilder.Entity("BaseLibrary.Entities.Allergies", b =>
                {
                    b.Property<int>("Allergy_Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Allergy_Id"));

                    b.Property<string>("Allergy_Name")
                        .HasColumnType("nvarchar(450)");

                    b.HasKey("Allergy_Id");

                    b.HasIndex("Allergy_Name")
                        .IsUnique()
                        .HasFilter("[Allergy_Name] IS NOT NULL");

                    b.ToTable("Allergy");
                });

            modelBuilder.Entity("BaseLibrary.Entities.Dieticians", b =>
                {
                    b.Property<int>("Dietician_Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Dietician_Id"));

                    b.Property<string>("Description")
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("Maximum_Patient_Number")
                        .HasColumnType("int");

                    b.Property<int>("Patient_Number")
                        .HasColumnType("int");

                    b.Property<int?>("User_Id")
                        .HasColumnType("int");

                    b.HasKey("Dietician_Id");

                    b.HasIndex("User_Id")
                        .IsUnique()
                        .HasFilter("[User_Id] IS NOT NULL");

                    b.ToTable("Dieticians");
                });

            modelBuilder.Entity("BaseLibrary.Entities.Diseases", b =>
                {
                    b.Property<int>("Disease_Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Disease_Id"));

                    b.Property<string>("Diagnosis_Date")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Disease_Name")
                        .HasColumnType("nvarchar(450)");

                    b.HasKey("Disease_Id");

                    b.HasIndex("Disease_Name")
                        .IsUnique()
                        .HasFilter("[Disease_Name] IS NOT NULL");

                    b.ToTable("Diseases");
                });

            modelBuilder.Entity("BaseLibrary.Entities.Foods", b =>
                {
                    b.Property<int>("Food_Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Food_Id"));

                    b.Property<double>("Calorie")
                        .HasColumnType("float");

                    b.Property<double>("Carbohydrate")
                        .HasColumnType("float");

                    b.Property<double>("Fat")
                        .HasColumnType("float");

                    b.Property<string>("Food_Name")
                        .IsRequired()
                        .HasColumnType("nvarchar(450)");

                    b.Property<double>("Protein")
                        .HasColumnType("float");

                    b.HasKey("Food_Id");

                    b.HasIndex("Food_Name")
                        .IsUnique();

                    b.ToTable("Foods");
                });

            modelBuilder.Entity("BaseLibrary.Entities.Ingredient", b =>
                {
                    b.Property<int>("Ingredient_Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Ingredient_Id"));

                    b.Property<double?>("Calorie")
                        .HasColumnType("float");

                    b.Property<double?>("Carbohydrate")
                        .HasColumnType("float");

                    b.Property<double?>("Fat")
                        .HasColumnType("float");

                    b.Property<string>("Ingredient_Name")
                        .HasColumnType("nvarchar(450)");

                    b.Property<double?>("Protein")
                        .HasColumnType("float");

                    b.HasKey("Ingredient_Id");

                    b.HasIndex("Ingredient_Name")
                        .IsUnique()
                        .HasFilter("[Ingredient_Name] IS NOT NULL");

                    b.ToTable("Ingredients");
                });

            modelBuilder.Entity("BaseLibrary.Entities.Medication", b =>
                {
                    b.Property<int>("Medication_Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Medication_Id"));

                    b.Property<string>("Medication_Name")
                        .HasColumnType("nvarchar(450)");

                    b.HasKey("Medication_Id");

                    b.HasIndex("Medication_Name")
                        .IsUnique()
                        .HasFilter("[Medication_Name] IS NOT NULL");

                    b.ToTable("Medications");
                });

            modelBuilder.Entity("BaseLibrary.Entities.Menus", b =>
                {
                    b.Property<int>("Menu_Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Menu_Id"));

                    b.Property<DateTime>("Created_At")
                        .HasColumnType("datetime2");

                    b.Property<string>("Created_by")
                        .HasColumnType("nvarchar(max)");

                    b.Property<int?>("Dietician_Id")
                        .HasColumnType("int");

                    b.Property<DateTime?>("Menu_End")
                        .HasColumnType("datetime2");

                    b.Property<string>("Menu_Name")
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime?>("Menu_Start")
                        .HasColumnType("datetime2");

                    b.Property<string>("Notes")
                        .HasColumnType("nvarchar(max)");

                    b.Property<int?>("Patient_Id")
                        .HasColumnType("int");

                    b.Property<DateTime?>("Updated_At")
                        .HasColumnType("datetime2");

                    b.HasKey("Menu_Id");

                    b.HasIndex("Dietician_Id");

                    b.HasIndex("Patient_Id");

                    b.ToTable("Menus");
                });

            modelBuilder.Entity("BaseLibrary.Entities.Patients", b =>
                {
                    b.Property<int>("Patient_Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Patient_Id"));

                    b.Property<DateTime?>("Assign_Date")
                        .HasColumnType("datetime2");

                    b.Property<DateTime?>("Assign_Delete_Date")
                        .HasColumnType("datetime2");

                    b.Property<DateTime?>("Assign_Update_Date")
                        .HasColumnType("datetime2");

                    b.Property<int?>("Created_By_Admin_Id")
                        .HasColumnType("int");

                    b.Property<string>("Description")
                        .HasColumnType("nvarchar(max)");

                    b.Property<int?>("Dietician_Id")
                        .HasColumnType("int");

                    b.Property<DateTime?>("DoB")
                        .HasColumnType("datetime2");

                    b.Property<string>("Gender")
                        .HasColumnType("nvarchar(max)");

                    b.Property<byte?>("Height")
                        .HasColumnType("tinyint");

                    b.Property<bool?>("Is_profile_completed")
                        .HasColumnType("bit");

                    b.Property<int?>("User_Id")
                        .HasColumnType("int");

                    b.Property<short?>("Weight")
                        .HasColumnType("smallint");

                    b.HasKey("Patient_Id");

                    b.HasIndex("Dietician_Id");

                    b.HasIndex("User_Id")
                        .IsUnique()
                        .HasFilter("[User_Id] IS NOT NULL");

                    b.ToTable("Patients");
                });

            modelBuilder.Entity("BaseLibrary.Entities.RefreshTokenInfo", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<string>("Token")
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("UserId")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("UserId");

                    b.ToTable("RefreshTokenInfos");
                });

            modelBuilder.Entity("BaseLibrary.Entities.Roles", b =>
                {
                    b.Property<int>("Role_Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Role_Id"));

                    b.Property<string>("Role_Name")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Role_Id");

                    b.ToTable("Roles");
                });

            modelBuilder.Entity("BaseLibrary.Entities.Users", b =>
                {
                    b.Property<int>("User_Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("User_Id"));

                    b.Property<string>("Avatar_Url")
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime>("Created_At")
                        .HasColumnType("datetime2");

                    b.Property<DateTime?>("Deleted_At")
                        .HasColumnType("datetime2");

                    b.Property<string>("Email")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("Mobile")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Password")
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime?>("Updated_At")
                        .HasColumnType("datetime2");

                    b.Property<string>("User_Name")
                        .HasColumnType("nvarchar(450)");

                    b.HasKey("User_Id");

                    b.HasIndex("Email")
                        .IsUnique()
                        .HasFilter("[Email] IS NOT NULL");

                    b.HasIndex("User_Name");

                    b.ToTable("Users");
                });

            modelBuilder.Entity("BaseLibrary.EntitiesRelation.Food_Allergies", b =>
                {
                    b.Property<int>("FoodId")
                        .HasColumnType("int");

                    b.Property<int>("AllergyId")
                        .HasColumnType("int");

                    b.HasKey("FoodId", "AllergyId");

                    b.HasIndex("AllergyId");

                    b.ToTable("Food_Allergies");
                });

            modelBuilder.Entity("BaseLibrary.EntitiesRelation.Food_Ingredients", b =>
                {
                    b.Property<int>("FoodId")
                        .HasColumnType("int");

                    b.Property<int>("IngredientId")
                        .HasColumnType("int");

                    b.HasKey("FoodId", "IngredientId");

                    b.HasIndex("IngredientId");

                    b.ToTable("Food_Ingredients");
                });

            modelBuilder.Entity("BaseLibrary.EntitiesRelation.Menu_Foods", b =>
                {
                    b.Property<int>("MenuId")
                        .HasColumnType("int");

                    b.Property<int>("FoodId")
                        .HasColumnType("int");

                    b.HasKey("MenuId", "FoodId");

                    b.HasIndex("FoodId");

                    b.ToTable("Menu_Foods");
                });

            modelBuilder.Entity("BaseLibrary.EntitiesRelation.Patient_Allergies", b =>
                {
                    b.Property<int>("PatientId")
                        .HasColumnType("int");

                    b.Property<int>("AllergyId")
                        .HasColumnType("int");

                    b.HasKey("PatientId", "AllergyId");

                    b.HasIndex("AllergyId");

                    b.ToTable("Patient_Allergies");
                });

            modelBuilder.Entity("BaseLibrary.EntitiesRelation.Patient_Disease", b =>
                {
                    b.Property<int>("PatientId")
                        .HasColumnType("int");

                    b.Property<int>("DiseaseId")
                        .HasColumnType("int");

                    b.HasKey("PatientId", "DiseaseId");

                    b.HasIndex("DiseaseId");

                    b.ToTable("Patient_Diseases");
                });

            modelBuilder.Entity("BaseLibrary.EntitiesRelation.Patient_Medication", b =>
                {
                    b.Property<int>("PatientId")
                        .HasColumnType("int");

                    b.Property<int>("MedicationId")
                        .HasColumnType("int");

                    b.HasKey("PatientId", "MedicationId");

                    b.HasIndex("MedicationId");

                    b.ToTable("patient_Medications");
                });

            modelBuilder.Entity("BaseLibrary.EntitiesRelation.User_Roles", b =>
                {
                    b.Property<int>("UserId")
                        .HasColumnType("int");

                    b.Property<int>("RoleId")
                        .HasColumnType("int");

                    b.HasKey("UserId", "RoleId");

                    b.HasIndex("RoleId");

                    b.ToTable("User_Roles");
                });

            modelBuilder.Entity("BaseLibrary.Entities.Admins", b =>
                {
                    b.HasOne("BaseLibrary.Entities.Users", "Users")
                        .WithOne("Admins")
                        .HasForeignKey("BaseLibrary.Entities.Admins", "User_Id")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Users");
                });

            modelBuilder.Entity("BaseLibrary.Entities.Dieticians", b =>
                {
                    b.HasOne("BaseLibrary.Entities.Users", "Users")
                        .WithOne("Dieticians")
                        .HasForeignKey("BaseLibrary.Entities.Dieticians", "User_Id");

                    b.Navigation("Users");
                });

            modelBuilder.Entity("BaseLibrary.Entities.Menus", b =>
                {
                    b.HasOne("BaseLibrary.Entities.Dieticians", "dietician")
                        .WithMany()
                        .HasForeignKey("Dietician_Id");

                    b.HasOne("BaseLibrary.Entities.Patients", "patient")
                        .WithMany()
                        .HasForeignKey("Patient_Id");

                    b.Navigation("dietician");

                    b.Navigation("patient");
                });

            modelBuilder.Entity("BaseLibrary.Entities.Patients", b =>
                {
                    b.HasOne("BaseLibrary.Entities.Dieticians", "Dieticians")
                        .WithMany()
                        .HasForeignKey("Dietician_Id");

                    b.HasOne("BaseLibrary.Entities.Users", "Users")
                        .WithOne("Patients")
                        .HasForeignKey("BaseLibrary.Entities.Patients", "User_Id");

                    b.Navigation("Dieticians");

                    b.Navigation("Users");
                });

            modelBuilder.Entity("BaseLibrary.Entities.RefreshTokenInfo", b =>
                {
                    b.HasOne("BaseLibrary.Entities.Users", "Users")
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Users");
                });

            modelBuilder.Entity("BaseLibrary.EntitiesRelation.Food_Allergies", b =>
                {
                    b.HasOne("BaseLibrary.Entities.Allergies", "Allergy")
                        .WithMany("FoodAllergies")
                        .HasForeignKey("AllergyId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("BaseLibrary.Entities.Foods", "Food")
                        .WithMany("FoodAllergies")
                        .HasForeignKey("FoodId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Allergy");

                    b.Navigation("Food");
                });

            modelBuilder.Entity("BaseLibrary.EntitiesRelation.Food_Ingredients", b =>
                {
                    b.HasOne("BaseLibrary.Entities.Foods", "Food")
                        .WithMany("FoodIngredients")
                        .HasForeignKey("FoodId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("BaseLibrary.Entities.Ingredient", "Ingredient")
                        .WithMany("FoodIngredients")
                        .HasForeignKey("IngredientId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Food");

                    b.Navigation("Ingredient");
                });

            modelBuilder.Entity("BaseLibrary.EntitiesRelation.Menu_Foods", b =>
                {
                    b.HasOne("BaseLibrary.Entities.Foods", "Food")
                        .WithMany("MenuFoods")
                        .HasForeignKey("FoodId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("BaseLibrary.Entities.Menus", "Menu")
                        .WithMany("MenuFoods")
                        .HasForeignKey("MenuId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Food");

                    b.Navigation("Menu");
                });

            modelBuilder.Entity("BaseLibrary.EntitiesRelation.Patient_Allergies", b =>
                {
                    b.HasOne("BaseLibrary.Entities.Allergies", "Allergy")
                        .WithMany("PatientAllergies")
                        .HasForeignKey("AllergyId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("BaseLibrary.Entities.Patients", "Patient")
                        .WithMany("PatientAllergies")
                        .HasForeignKey("PatientId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Allergy");

                    b.Navigation("Patient");
                });

            modelBuilder.Entity("BaseLibrary.EntitiesRelation.Patient_Disease", b =>
                {
                    b.HasOne("BaseLibrary.Entities.Diseases", "Disease")
                        .WithMany("PatientDiseases")
                        .HasForeignKey("DiseaseId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("BaseLibrary.Entities.Patients", "Patient")
                        .WithMany("PatientDiseases")
                        .HasForeignKey("PatientId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Disease");

                    b.Navigation("Patient");
                });

            modelBuilder.Entity("BaseLibrary.EntitiesRelation.Patient_Medication", b =>
                {
                    b.HasOne("BaseLibrary.Entities.Medication", "Medication")
                        .WithMany("PatientMedications")
                        .HasForeignKey("MedicationId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("BaseLibrary.Entities.Patients", "Patient")
                        .WithMany("PatientMedications")
                        .HasForeignKey("PatientId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Medication");

                    b.Navigation("Patient");
                });

            modelBuilder.Entity("BaseLibrary.EntitiesRelation.User_Roles", b =>
                {
                    b.HasOne("BaseLibrary.Entities.Roles", "Role")
                        .WithMany("Users")
                        .HasForeignKey("RoleId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("BaseLibrary.Entities.Users", "User")
                        .WithMany("UserRoles")
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Role");

                    b.Navigation("User");
                });

            modelBuilder.Entity("BaseLibrary.Entities.Allergies", b =>
                {
                    b.Navigation("FoodAllergies");

                    b.Navigation("PatientAllergies");
                });

            modelBuilder.Entity("BaseLibrary.Entities.Diseases", b =>
                {
                    b.Navigation("PatientDiseases");
                });

            modelBuilder.Entity("BaseLibrary.Entities.Foods", b =>
                {
                    b.Navigation("FoodAllergies");

                    b.Navigation("FoodIngredients");

                    b.Navigation("MenuFoods");
                });

            modelBuilder.Entity("BaseLibrary.Entities.Ingredient", b =>
                {
                    b.Navigation("FoodIngredients");
                });

            modelBuilder.Entity("BaseLibrary.Entities.Medication", b =>
                {
                    b.Navigation("PatientMedications");
                });

            modelBuilder.Entity("BaseLibrary.Entities.Menus", b =>
                {
                    b.Navigation("MenuFoods");
                });

            modelBuilder.Entity("BaseLibrary.Entities.Patients", b =>
                {
                    b.Navigation("PatientAllergies");

                    b.Navigation("PatientDiseases");

                    b.Navigation("PatientMedications");
                });

            modelBuilder.Entity("BaseLibrary.Entities.Roles", b =>
                {
                    b.Navigation("Users");
                });

            modelBuilder.Entity("BaseLibrary.Entities.Users", b =>
                {
                    b.Navigation("Admins");

                    b.Navigation("Dieticians");

                    b.Navigation("Patients");

                    b.Navigation("UserRoles");
                });
#pragma warning restore 612, 618
        }
    }
}
