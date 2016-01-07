﻿using System.Collections.Generic;
using System.Linq;
using Enums.Enums;
using HandleToolsInterfaces.RepositoryHandlers;
using StorageModels.Interfaces;

namespace HandleTools.RepositoryHandlers
{
    public class TwoSideShowingHandler<T> : ITwoSideShowingHandler<T>
        where T: IIdModel, IShowStatusModel
    {
        public IEnumerable<T> GetFromSideModels(IEnumerable<T> list)
        {
            var result =  list.Where(arg => arg.ShowStatus == TwoSideShowStatus.FromSideOnly || arg.ShowStatus == TwoSideShowStatus.Showed);
            return result;
        }

        public IEnumerable<T> GetToSideModels(IEnumerable<T> list)
        {
            var result = list.Where(arg => arg.ShowStatus == TwoSideShowStatus.ToSideOnly || arg.ShowStatus == TwoSideShowStatus.Showed);
            return result;
        }

        public IEnumerable<T> GetHiddenModels(IEnumerable<T> list)
        {
            var result = list.Where(arg => arg.ShowStatus == TwoSideShowStatus.Hidden);
            return result;
        }

        public T HideModeFromFromSide(T model)
        {
            if (model.ShowStatus == TwoSideShowStatus.Showed)
            {
                model.ShowStatus = TwoSideShowStatus.ToSideOnly;
            }

            if (model.ShowStatus == TwoSideShowStatus.FromSideOnly)
            {
                model.ShowStatus = TwoSideShowStatus.Hidden;
            }

            return model;
        }

        public T HideModelFromToSide(T model)
        {
            if (model.ShowStatus == TwoSideShowStatus.Showed)
            {
                model.ShowStatus = TwoSideShowStatus.FromSideOnly;
            }

            if (model.ShowStatus == TwoSideShowStatus.ToSideOnly)
            {
                model.ShowStatus = TwoSideShowStatus.Hidden;
            }

            return model;
        }

        public T HideModel(T model)
        {
            model.ShowStatus = TwoSideShowStatus.Hidden;

            return model;
        }

        public T ShowModelForFromSide(T model)
        {
            if (model.ShowStatus == TwoSideShowStatus.Hidden)
            {
                model.ShowStatus = TwoSideShowStatus.FromSideOnly;
            }

            if (model.ShowStatus == TwoSideShowStatus.ToSideOnly)
            {
                model.ShowStatus = TwoSideShowStatus.Showed;
            }

            return model;
        }

        public T ShowModelForToSide(T model)
        {
            if (model.ShowStatus == TwoSideShowStatus.Hidden)
            {
                model.ShowStatus = TwoSideShowStatus.ToSideOnly;
            }

            if (model.ShowStatus == TwoSideShowStatus.FromSideOnly)
            {
                model.ShowStatus = TwoSideShowStatus.Showed;
            }

            return model;
        }

        public T ShowModel(T model)
        {
            model.ShowStatus = TwoSideShowStatus.Showed;

            return model;
        }
    }
}
