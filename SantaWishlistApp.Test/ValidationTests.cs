using System.ComponentModel.DataAnnotations;
using SantasWishlist.Domain;
using SantaWishlist.Data.Models;
using SantaWishlistApp.Areas.Child.Models;
using SantaWishlistApp.Areas.Child.Models.Validations;
using Xunit;

namespace SantaWishlistApp.Test;

public class ValidationTests
{
    [Fact]
    public void NightLight_ShouldReturnFalse()
    {
        var attribute = new NightLight();
        var model = new GiftChooseViewModel();
        var context = new ValidationContext(model);
        
        var selectedGifts = new Dictionary<GiftCategory, List<GiftCheckboxModel>>();
        var musicInstrument = new Gift()
        {
            Category = GiftCategory.WANT,
            Name = "muziekinstrument"
        };
        
        var underwear = new Gift()
        {
            Category = GiftCategory.WEAR,
            Name = "ondergoed"
        };
        
        var giftCheckBoxModelInstrument = new GiftCheckboxModel()
        {
            Gift = musicInstrument,
            IsChecked = true
        };

        var giftCheckBoxModelUnderwear = new GiftCheckboxModel()
        {
            Gift = underwear,
            IsChecked = true
        };
        
        var giftCheckList = new List<GiftCheckboxModel>();
        giftCheckList.Add(giftCheckBoxModelInstrument);
        giftCheckList.Add(giftCheckBoxModelUnderwear);

        selectedGifts[GiftCategory.WANT] = giftCheckList;
        model.SelectedGifts = selectedGifts;


        var result = attribute.IsInvalid(model, context);
        Assert.False(result);
    }
    
   
    [Fact]
    public void CustomRule_ShouldReturnFalse()
    {
        var attribute = new CustomRule();
        var model = new GiftChooseViewModel();
        var context = new ValidationContext(model);
        
        var selectedGifts = new Dictionary<GiftCategory, List<GiftCheckboxModel>>();
        var computerGame = new Gift()
        {
            Category = GiftCategory.WANT,
            Name = "computerspel"
        };
        
        var tutoring = new Gift()
        {
            Category = GiftCategory.WEAR,
            Name = "bijles"
        };
        
        var giftCheckBoxModelGame = new GiftCheckboxModel()
        {
            Gift = computerGame,
            IsChecked = true
        };

        var giftCheckBoxModelTutoring = new GiftCheckboxModel()
        {
            Gift = tutoring,
            IsChecked = true
        };
        
        var giftCheckList = new List<GiftCheckboxModel>();
        giftCheckList.Add(giftCheckBoxModelGame);
        giftCheckList.Add(giftCheckBoxModelTutoring);

        selectedGifts[GiftCategory.WANT] = giftCheckList;
        model.SelectedGifts = selectedGifts;


        var result = attribute.IsInvalid(model, context);
        Assert.False(result);
    }
    
   
    [Fact]
    public void ErkansExtraRule_ShouldReturnFalse()
    {
        var attribute = new ErkansExtraRule();
        var model = new GiftChooseViewModel();
        var context = new ValidationContext(model);
        
        var selectedGifts = new Dictionary<GiftCategory, List<GiftCheckboxModel>>();
        var readGift = new Gift()
        {
            Category = GiftCategory.READ,
            Name = "boek"
        };
        
        var nightLight = new Gift()
        {
            Category = GiftCategory.WEAR,
            Name = "nachtlampje"
        };
        
        var giftCheckBoxModelRead = new GiftCheckboxModel()
        {
            Gift = readGift,
            IsChecked = true
        };

        var giftCheckBoxModelLight = new GiftCheckboxModel()
        {
            Gift = nightLight,
            IsChecked = true
        };
        
        var giftCheckList = new List<GiftCheckboxModel>();
        giftCheckList.Add(giftCheckBoxModelRead);
        giftCheckList.Add(giftCheckBoxModelLight);

        selectedGifts[GiftCategory.READ] = giftCheckList;
        model.SelectedGifts = selectedGifts;


        var result = attribute.IsInvalid(model, context);
        Assert.False(result);
    }
    
  
    [Fact]
    public void LegoAndKnex_ShouldReturnFalse()
    {
        var attribute = new LegoAndKnex();
        var model = new GiftChooseViewModel();
        var context = new ValidationContext(model);
        
        var selectedGifts = new Dictionary<GiftCategory, List<GiftCheckboxModel>>();
        var knexGift = new Gift()
        {
            Category = GiftCategory.WANT,
            Name = "k'nex"
        };
        
        var legoGift = new Gift()
        {
            Category = GiftCategory.WANT,
            Name = "lego"
        };
        
        var giftCheckBoxModelKnex = new GiftCheckboxModel()
        {
            Gift = knexGift,
            IsChecked = true
        };

        var giftCheckBoxModelLego = new GiftCheckboxModel()
        {
            Gift = legoGift,
            IsChecked = true
        };
        
        var giftCheckList = new List<GiftCheckboxModel>();
        giftCheckList.Add(giftCheckBoxModelKnex);
        giftCheckList.Add(giftCheckBoxModelLego);

        selectedGifts[GiftCategory.READ] = giftCheckList;
        model.SelectedGifts = selectedGifts;


        var result = attribute.IsInvalid(model, context);
        Assert.False(result);
    }
    
   
    [Fact]
    public void SelectedInstrument_ShouldReturnFalse()
    {
        var attribute = new SelectedInstrument();
        var model = new GiftChooseViewModel();
        var context = new ValidationContext(model);
        
        var selectedGifts = new Dictionary<GiftCategory, List<GiftCheckboxModel>>();
        var musicGift = new Gift()
        {
            Category = GiftCategory.WANT,
            Name = "muziekinstrument"
        };
        
        var earPlugsGift = new Gift()
        {
            Category = GiftCategory.WANT,
            Name = "oordopjes"
        };
        
        var giftCheckBoxModelInstrument = new GiftCheckboxModel()
        {
            Gift = musicGift,
            IsChecked = true
        };

        var giftCheckBoxModelEarplugs = new GiftCheckboxModel()
        {
            Gift = earPlugsGift,
            IsChecked = true
        };
        
        var giftCheckList = new List<GiftCheckboxModel>();
        giftCheckList.Add(giftCheckBoxModelInstrument);
        giftCheckList.Add(giftCheckBoxModelEarplugs);

        selectedGifts[GiftCategory.READ] = giftCheckList;
        model.SelectedGifts = selectedGifts;


        var result = attribute.IsInvalid(model, context);
        Assert.False(result);
    }
}