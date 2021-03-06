﻿using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;
using StorageModels.Models.UserModels;

namespace DataBaseModelConfigurations.Configurations.UserModels
{
    public class UserDataBaseConfiguration: EntityTypeConfiguration<UserStorageModel>
    {
        public UserDataBaseConfiguration()
        {
            // Table name

            this.ToTable("Users");

            // Primary key

            this.HasKey(model => model.Id);

            // Properties

            this.Property(model => model.Id).HasDatabaseGeneratedOption(DatabaseGeneratedOption.Identity);
            this.Property(model => model.Name).IsRequired();

            // Links to tables

            this.HasMany(model => model.UserFunctions).WithRequired(link => link.User).HasForeignKey(model => model.UserId);
            this.HasMany(model => model.MessagesTo).WithRequired(link => link.UserTo).HasForeignKey(model => model.UserToId).WillCascadeOnDelete(false);
            this.HasMany(model => model.MessagesFrom).WithRequired(link => link.UserFrom).HasForeignKey(model => model.UserFromId).WillCascadeOnDelete(false);
            this.HasOptional(model => model.Account).WithRequired(link => link.User);
            this.HasOptional(model => model.ClinicUser).WithRequired(link => link.User);
            this.HasOptional(model => model.HospitalUser).WithRequired(link => link.User);
            this.HasRequired(model => model.UserType).WithMany(model => model.Users).HasForeignKey(model => model.UserTypeId);
            this.HasMany(model => model.Reservations).WithRequired(model => model.Reservator).HasForeignKey(model => model.ReservatorId);
            this.HasMany(model => model.BehalfReservations).WithOptional(model => model.BehalfReservator).HasForeignKey(model => model.BehalfReservatorId);
        }
    }
}
